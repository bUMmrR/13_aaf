using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aknakereső
{
    internal class cella
    {
        public int x, y, adjecent;
        public bool bomba,lathato;

        public cella(int x, int y, bool bomba)
        {
            this.x = x;
            this.y = y;
            this.bomba = bomba;
            lathato = false;
            adjecent = 0;
        }
    }
}
