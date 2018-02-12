using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class ParentViewItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Children { get; set; }
        public override string ToString()
        {
            return Name;
        }

    }
}
