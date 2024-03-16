using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    class Dame : Figur
    {
        public Dame(Farbe f, Position p)
        {
            farbe = f;
            // wert = todo
            if (farbe == Farbe.Schwarz)
            {
                bildpfad = "Dame_schwarz.gif";
            }
            else
            {
                bildpfad = "Dame_weiss.gif";
            }
            pos = p;
        }
        public override List<Zug> wasDarfFigur(Figur[,] figuren)
        {
            List<Zug> resultate = Helpers.wasDarfTurm(figuren, this); 
            resultate.AddRange(Helpers.wasDarfLauefer(figuren, this));
            return resultate;
        }
    }
}
