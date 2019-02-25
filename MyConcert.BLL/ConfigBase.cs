using System;
using System.Collections.Generic;
using Puppy.Model.Data;

namespace MyConcert.BLL
{
  public class ConfigBase
  {
    public static string DATABASE_PROVIDER = "DATABASE_PROVIDER";
    public static string ENV_MODE ="ENV_MODE";
    public static string CONNECTION_DEV ="CONNECTION_DEV";
    public static string CONNECTION_STG ="CONNECTION_STG";
    public static string CONNECTION_PRO ="CONNECTION_PRO";
    public static Dictionary<string,string> ConnectionStringList = new 
      Dictionary<string,string>
      {
        {"DEV",CONNECTION_DEV },
        {"STG", CONNECTION_STG},
        {"PRO", CONNECTION_PRO}
      };

    public static string GetConnectionString()
    {
      return DataConfiguration.GetConnectionString(
        Environment.GetEnvironmentVariable(ENV_MODE),
        ConnectionStringList
      );
    }
  }
}
