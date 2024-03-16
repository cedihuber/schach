using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    class Bauer:Figur
    {
        public Bauer (Farbe f, Position p)
        {
            farbe = f;
            if (farbe == Farbe.Schwarz)
            {
                bildpfad = "Bauer_schwarz.gif";
            }
            else
            {
                bildpfad = "Bauer_weiss.gif";
            }
            pos = p;
        }
        public override List<Zug> wasDarfFigur(Figur[,] figuren)
        {
            List<Zug> resultate = new List<Zug>();
            int richtung = farbe == Farbe.Weiss ? 1 : -1;
            int verwandlungsFeld = farbe == Farbe.Weiss ? 7 : 0;
            //erlaubte ziele
            List<Position> ziele = new List<Position>();
            //eins gerade aus
            if (figuren[pos.x, pos.y + richtung] == null)
            {
                ziele.Add(new Position(pos.x, pos.y + richtung));
            }
            //zwei gerade aus falls auf start feld
            if (pos.y==verwandlungsFeld-6*richtung && figuren[pos.x, pos.y + richtung] == null && figuren[pos.x, pos.y + 2*richtung] == null)
            {
                ziele.Add(new Position(pos.x, pos.y + 2*richtung));
            }
            //links schlagen
            if(pos.x>0 && figuren[pos.x - 1, pos.y + richtung] != null && figuren[pos.x-1,pos.y+richtung].farbe!=farbe)
            {
                ziele.Add(new Position(pos.x - 1, pos.y + richtung));
            }
            //rechts schlagen
            if (pos.x <7 && figuren[pos.x + 1, pos.y + richtung] != null && figuren[pos.x + 1, pos.y + richtung].farbe != farbe)
            {
                ziele.Add(new Position(pos.x + 1, pos.y + richtung));
            }
            foreach(Position z in ziele)
            {
                //damenverwandlung
                if(z.y == verwandlungsFeld)
                {
                    resultate.Add(new Zug(this, z, new Dame(farbe, z)));
                }
                else
                {
                    resultate.Add(new Zug(this, z));
                }
            }
            return resultate;
        }
    }
}
