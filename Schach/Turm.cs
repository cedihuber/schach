using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    class Turm : Figur
    {
        public Turm(Farbe f, Position p)
        {
            farbe = f;
            if (farbe == Farbe.Schwarz)
            {
                bildpfad = "Turm_schwarz.gif";
            }
            else
            {
                bildpfad = "Turm_weiss.gif";
            }
            pos = p;
        }
        public override List<Zug> wasDarfFigur(Figur[,] figuren)
        {
            return Helpers.wasDarfTurm(figuren, this);
        }
    }
}
