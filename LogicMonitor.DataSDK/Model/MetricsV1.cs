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
        public string k;
        public string v;
        public Dictionary<string, string> Values = new Dictionary<string, string>();

        public MetricsV1(Resource _resource, DataSource _dataSource, DataSourceInstance _dataSourceInstance, DataPoint _dataPoint, Dictionary<string, string> _values)
        {
            resource = _resource;
            dataSource = _dataSource;
            dataSourceInstance = _dataSourceInstance;
            dataPoint = _dataPoint;
            foreach (var item in _values)
            {
                Values.Add(item.Key,item.Value);
            }

        }
    }
}