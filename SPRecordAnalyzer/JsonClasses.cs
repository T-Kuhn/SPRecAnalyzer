using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPRecordAnalyzer
{
    public class PixelObject
    {
        public List<object> PointArr { get; set; }
        public int ID { get; set; }
        public int EntryIndex { get; set; }
        public List<int> CenterPos { get; set; }
    }

    public class PixelObjectArr
    {
        public List<PixelObject> PixelObjects { get; set; }
    }

    public class RootObject
    {
        public PixelObjectArr PixelObjectArr { get; set; }
    }
}
