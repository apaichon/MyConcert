using System;
using Puppy.DAL;
using Puppy.BLL;
using Puppy.Model;
using Puppy.Model.Data;
using Puppy.Model.Business;
using Puppy.Model.Message;
using Puppy.Model.Output;
using MongoDB.Driver;

namespace MyConcert.BLL
{
    public class ConcertSeatsBLL:MyConcertBase
    {
        public ConcertSeatsBLL()
        {
            this.DbConfig.TableName ="concertSeats";
            
        }
        
        public Result GetAvailableSeats(string concertId, IMessage msg)
        {
             AggregatePipelineModel model = new AggregatePipelineModel();
             model.Match = "{'concertId':'{0}', 'bookingStatus._id': 'bs01'}".Replace("{0}",concertId);
             model.Group = "{ '_id':  {'zoneId':'$zone._id', 'zone': '$zone.zone', 'price': '$zone.price'}, 'totalAvailable':{ '$sum': 1 } }" ;
             model.Project = @"{
                    '_id': 0,
                     'zoneId': '$_id.zoneId',
                    'zone': '$_id.zone',
                    'price': '$_id.price',
                    'totalAvailable': '$totalAvailable'
                    }";
            model.Sort ="{'price':-1}";
            model.Lookup =  new LookupModel {
                ForeignCollectionName ="concertTicketZones",
                LocalFieldName = "zoneId",
                ForeignFieldName = "_id",
                ResultAs ="totalTickets"
            };
            

             return this.BizObject.Execute(
                this.DbConfig,
                BusinessOperator.Aggregate,
                model,
                msg
            );
        }

        public Result GetSeatsByZoneId(string zoneId, IMessage msg)
        {
            string condition = "{'zone._id':{0}}".Replace("{0}",zoneId);
            return this.BizObject.Execute(
                this.DbConfig,
                BusinessOperator.Get,
                condition,
                msg
            );
        }

        public Result GetAvailableSeatsByZoneId(string zoneId, IMessage msg)
        {
            string condition = "{'zone._id':{0}, 'bookingStatus._id':'bs01'}".Replace("{0}",zoneId);
            return this.BizObject.Execute(
                this.DbConfig,
                BusinessOperator.Get,
                condition,
                msg
            );
        }

        public Result BookingSeat(int totalSeats, string seatIds, string jsonBooked, IMessage msg)
        {
            Result result;
            using (IDataAdapter da =  DataManager.Build(this.DbConfig))
            {
                IClientSessionHandle Session = null;
                try 
                {
                    string tickets = "";
                    string seatUnavailable = @"
                    {
                        _id: {'$in': <ids>},
                        'bookingStatus._id': {'$ne': 'bs01'}
                    }
                    ".Replace("<ids>", seatIds);
                    Session = da.Open()
                    .CreateTransaction();
                    Session.StartTransaction();
                    da.Get(seatUnavailable, out tickets);

                    if(tickets != "[]" ) {
                        Session.AbortTransaction();
                        throw new System.InvalidOperationException("Unavailable ticket:"+ tickets);
                    }

                    string bookSeats =@"
                    {
                        '_id': {'$in': <ids>}
                    }
                    ".Replace("<ids>", seatIds);

                    long total = 0 ;
                    da.EditMany(bookSeats, jsonBooked, out total );
                   if (totalSeats != total)
                    {
                        Session.AbortTransaction();
                        throw new System.InvalidOperationException("Unavailable book all ticktes!");
                    }

                    Session.CommitTransaction();
                    result = Result.GetResult(BusinessStatus.Completed,210,msg, "Booking is successfully.");

                }
                catch (Exception err)
                {
                    if (Session !=null)
                    {
                        Session.AbortTransaction();
                    }
                    result = Result.GetResult(BusinessStatus.Completed,500,msg, err.Message);
                }
                
            }

            return result;
        }

        public Result GetTicketByOwner(string userId, IMessage msg)
        {
            string condition = "{'bookingStatus.bookedBy':'{0}'}".Replace("{0}",userId);
            return this.BizObject.Execute(
                this.DbConfig,
                BusinessOperator.Get,
                condition,
                msg
            );
        }




    }
}
