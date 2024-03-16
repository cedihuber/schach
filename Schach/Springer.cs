using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    class Springer : Figur
    {
        public Springer(Farbe f, Position p)
        {
            farbe = f;
            // wert = todo
            if (farbe == Farbe.Schwarz)
            {
                bildpfad = "Springer_schwarz.gif";
            }
            else
            {
                bildpfad = "Springer_weiss.gif";
            }
            pos = p;
        }
        public override List<Zug> wasDarfFigur(Figur[,] figuren)
        {
            List<Zug> resultate = new List<Zug>();
            List<Position> ziele = new List<Position>();
            ziele.Add(new Position(pos.x + 1, pos.y + 2));
            ziele.Add(new Position(pos.x - 1, pos.y + 2));
            ziele.Add(new Position(pos.x + 1, pos.y - 2));
            ziele.Add(new Position(pos.x - 1, pos.y - 2));
            ziele.Add(new Position(pos.x + 2, pos.y + 1));
            ziele.Add(new Position(pos.x + 2, pos.y - 1));
            ziele.Add(new Position(pos.x - 2, pos.y + 1));
            ziele.Add(new Position(pos.x - 2, pos.y - 1));
            foreach(Position z in ziele)
            {
                if(Helpers.imFeld(z) && (figuren[z.x, z.y] == null || farbe != figuren[z.x, z.y].farbe))
                {
                    resultate.Add(new Zug(this, z));
                }
            }
            return resultate;
        }
    }
}
