using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    class Laeufer : Figur
    {
        public Laeufer(Farbe f, Position p)
        {
            farbe = f;
            if (farbe == Farbe.Schwarz)
            {
                bildpfad = "Laeufer_schwarz.gif";
            }
            else
            {
                bildpfad = "Laeufer_weiss.gif";
            }
            pos = p;
        }
        public override List<Zug> wasDarfFigur(Figur[,] figuren)
        {
            return Helpers.wasDarfLauefer(figuren, this);
        }
    }
}
