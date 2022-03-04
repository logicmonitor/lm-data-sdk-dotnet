using System;
using System.Collections.Generic;
namespace LogicMonitor.DataSDK.Model
{
    public class MetricsV1:IInput
    {
        
        public Resource resource;
        public DataSource dataSource;
        public DataSourceInstance dataSourceInstance;
        public DataPoint dataPoint;
        public Dictionary<string, string> values;

        public MetricsV1()
        {
            resource = new Resource();
            dataSource = new DataSource();
            dataSourceInstance = new DataSourceInstance();
            dataPoint = new DataPoint();
            values = new Dictionary<string, string>();
        }
    }
}