using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace EpForceDirectedGraph
{
    class DoubleBufferPanel : Panel
    {

        public DoubleBufferPanel()
            : base()
        {






            this.DoubleBuffered = true;


            this.UpdateStyles();

        }

        
    }

}
