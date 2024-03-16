using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schach
{
    public enum Farbe
    {
        Schwarz,
        Weiss
    }
    static class Helpers
    {
        public static List<Zug> wasDarfLauefer(Figur[,] figuren, Figur laeufer)
        {
            List<Zug> resultate = new List<Zug>();
            for (int yrichtung = -1; yrichtung <= 1; yrichtung += 2)
            {
                for (int xrichtung = -1; xrichtung <= 1; xrichtung += 2)
                {
                    int x = laeufer.pos.x + xrichtung;
                    int y = laeufer.pos.y + yrichtung;
                    while (x >= 0 && x <= 7 && y <= 7 && y >= 0)
                    {
                        if (figuren[x, y] == null)
                        {
                            resultate.Add(new Zug(laeufer, new Position(x, y)));
                        }
                        else
                        {
                            if (laeufer.farbe != figuren[x, y].farbe)
                            {
                                resultate.Add(new Zug(laeufer, new Position(x, y)));
                            }
                            break;
                        }
                        y += yrichtung;
                        x += xrichtung;
                    }
                }
            }
            return resultate;
        }
        public static List<Zug> wasDarfTurm(Figur[,] figuren, Figur turm)
        {
            List<Zug> resultate = new List<Zug>();
            for (int richtung = -1; richtung <= 1; richtung += 2)
            {
                int x = turm.pos.x + richtung;
                while (x >= 0 && x <= 7)
                {
                    if (figuren[x, turm.pos.y] == null)
                    {
                        resultate.Add(new Zug(turm, new Position(x, turm.pos.y)));
                    }
                    else
                    {
                        if (turm.farbe != figuren[x, turm.pos.y].farbe)
                        {
                            resultate.Add(new Zug(turm, new Position(x, turm.pos.y)));
                        }
                        break;
                    }
                    x += richtung;
                }
            }
            for (int richtung = -1; richtung <= 1; richtung += 2)
            {
                int y = turm.pos.y + richtung;
                while (y >= 0 && y <= 7)
                {
                    if (figuren[turm.pos.x, y] == null)
                    {
                        resultate.Add(new Zug(turm, new Position(turm.pos.x, y)));
                    }
                    else
                    {
                        if (turm.farbe != figuren[turm.pos.x, y].farbe)
                        {
                            resultate.Add(new Zug(turm, new Position(turm.pos.x, y)));
                        }
                        break;
                    }
                    y += richtung;

                }
            }
            return resultate;
        }
        static public bool imFeld(Position p)
        {
            return p.x >= 0 && p.x <= 7 && p.y >= 0 && p.y <= 7;
        }
    }


    class Position
    {
        public Position(int x_, int y_)
        {
            x = x_;
            y = y_;
        }
        public int x;
        public int y;
        public static bool operator==(Position a, Position b)
        {
            return a.x == b.x && a.y == b.y;
        }
        public static bool operator !=(Position a, Position b)
        {
            return !(a==b);
        }
    }
    abstract class Figur
    {
        public int wert;
        public string bildpfad;
        public Farbe farbe;
        public Position pos;
        //public void move() { }
        abstract public List<Zug> wasDarfFigur(Figur[,] figuren);
    }
}