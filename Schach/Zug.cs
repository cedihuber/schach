using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    class Zug
    {
        public Figur figur;
        public Figur zielFigur;
        public Position ziel;
        public Position ursprung;
        public Figur geschlagen;
        public Zug(Figur f, Position z)
        {
            figur = f;
            zielFigur = f;
            ziel = z;
            geschlagen = null;
        }
        public Zug(Figur f, Position z, Figur zielF)
        {
            figur = f;
            zielFigur = zielF;
            ziel = z;
            geschlagen = null;
        }
        //figur an ziel stellen
        public void ausfueren(Figur[,] figuren)
        {
            if(figuren[ziel.x, ziel.y] != null)
            {
                geschlagen = figuren[ziel.x, ziel.y];
                geschlagen.pos = new Position(-1, -1);
            }
            ursprung = figur.pos;
            figuren[ziel.x, ziel.y] = zielFigur;
            figuren[ursprung.x, ursprung.y] = null;
            zielFigur.pos = ziel;
        }
        //ein zug rückgängig machen
        public void rueckgaengig(Figur[,] figuren)
        {
            figuren[ursprung.x, ursprung.y] = figur;
            figur.pos = ursprung;
            if (geschlagen == null)
            {
                figuren[ziel.x, ziel.y] = null;
            }
            else
            {
                figuren[ziel.x, ziel.y] = geschlagen;
                geschlagen.pos = ziel;
            }
        }
    }
}
