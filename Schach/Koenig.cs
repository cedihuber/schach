using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    class Koenig : Figur
    {
        public Koenig(Farbe f, Position p)
        {
            farbe = f;
            // wert = todo
            if (farbe == Farbe.Schwarz)
            {
                bildpfad = "Koenig_schwarz.gif";
            }
            else
            {
                bildpfad = "Koenig_weiss.gif";
            }
            pos = p;
        }
        public override List<Zug> wasDarfFigur(Figur[,] figuren)
        {
            List<Zug> resultate = new List<Zug>();
            for(int x = -1; x<=1; x++)
            {
                for(int y = -1; y <= 1; y++)
                {
                    Position ziel = new Position(pos.x + x, pos.y + y);
                    if(ziel.x >=0 && ziel.x <= 7 && ziel.y >= 0 && ziel.y <= 7)
                    {
                        if (figuren[ziel.x, ziel.y] == null || farbe != figuren[ziel.x, ziel.y].farbe)
                        { 
                            resultate.Add(new Zug(this, ziel));
                        }
                    }
                }
            }
            return resultate;
        }
    }
}

