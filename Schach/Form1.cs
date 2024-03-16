using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schach
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Button[,] buttons = new Button[8, 8];
        Figur[,] figuren = new Figur[8, 8];
        Figur schwarzerKoenig;
        Figur weisserKoenig;
        bool auswahlSchritt = true;
        Figur ausgewaehlteFigur = null;
        Farbe spielendeFarbe = Farbe.Weiss;
        List<Zug> gespielteZuege = new List<Zug>();

        private void Form1_Load(object sender, EventArgs e)
        {
            //schachbrett erstellen
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            for (int x = 0; x <= 7; x++)
            {
                for (int y = 0; y <= 7; y++)
                {
                    Button b = new Button();
                    int buttonSize = (this.Height - 100) / 8;
                    int seitenAbstand = (this.Width - 8 * buttonSize) / 2;
                    b.Top = 50 + y * buttonSize;
                    b.Left = seitenAbstand + x * buttonSize;
                    b.Width = buttonSize;
                    b.Height = buttonSize;
                    if ((x + y) % 2 == 0)
                    {
                        b.BackColor = Color.White;
                    }
                    else
                    {
                        b.BackColor = Color.Black;
                    }
                    b.Text = "";
                    b.Click += new System.EventHandler(feldClick);
                    buttons[x, 7 - y] = b;
                    this.Controls.Add(b);
                }
            }
            //figuren erstellen und an die richtige position setzten
            List<Figur> tmpFiguren = new List<Figur>();
            tmpFiguren.Add(new Turm(Farbe.Weiss, new Position(0, 0)));
            tmpFiguren.Add(new Turm(Farbe.Weiss, new Position(7, 0)));
            tmpFiguren.Add(new Turm(Farbe.Schwarz, new Position(0, 7)));
            tmpFiguren.Add(new Turm(Farbe.Schwarz, new Position(7, 7)));

            for (int i = 0; i <= 7; i++)
            {
                tmpFiguren.Add(new Bauer(Farbe.Weiss, new Position(i, 1)));
                tmpFiguren.Add(new Bauer(Farbe.Schwarz, new Position(i, 6)));
            }

            tmpFiguren.Add(new Springer(Farbe.Weiss, new Position(1, 0)));
            tmpFiguren.Add(new Springer(Farbe.Weiss, new Position(6, 0)));
            tmpFiguren.Add(new Springer(Farbe.Schwarz, new Position(1, 7)));
            tmpFiguren.Add(new Springer(Farbe.Schwarz, new Position(6, 7)));

            tmpFiguren.Add(new Laeufer(Farbe.Weiss, new Position(2, 0)));
            tmpFiguren.Add(new Laeufer(Farbe.Weiss, new Position(5, 0)));
            tmpFiguren.Add(new Laeufer(Farbe.Schwarz, new Position(2, 7)));
            tmpFiguren.Add(new Laeufer(Farbe.Schwarz, new Position(5, 7)));

            tmpFiguren.Add(new Dame(Farbe.Weiss, new Position(3, 0)));
            tmpFiguren.Add(new Dame(Farbe.Schwarz, new Position(3, 7)));

            weisserKoenig = new Koenig(Farbe.Weiss, new Position(4, 0));
            schwarzerKoenig = new Koenig(Farbe.Schwarz, new Position(4, 7));
            tmpFiguren.Add(weisserKoenig);
            tmpFiguren.Add(schwarzerKoenig);

            foreach (Figur f in tmpFiguren)
            {
                figuren[f.pos.x, f.pos.y] = f;
            }
            bildaktualisieren();
        }
        //testen ob der könig schach steht
        public bool wirdKoenigAngegriffen(Farbe farbe)
        {
            for (int x = 0; x <= 7; x++)
            {
                for (int y = 0; y <= 7; y++)
                {
                    if (figuren[x, y] != null && figuren[x, y].farbe != farbe)
                    {
                        foreach (Zug z in figuren[x, y].wasDarfFigur(figuren))
                        {
                            if (farbe == Farbe.Weiss)
                            {
                                if (z.ziel == weisserKoenig.pos)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                if (z.ziel == schwarzerKoenig.pos)
                                {
                                    return true;
                                }
                            }
                        }

                    }
                }
            }
            return false;
        }
        //testen ob jemand gewonnen hat
        public bool hatFarbeGewonnen(Farbe farbe)
        {
            Farbe andereFarbe;
            Figur gegenerKoenig;
            if (farbe == Farbe.Weiss)
            {
                andereFarbe = Farbe.Schwarz;
                gegenerKoenig = schwarzerKoenig;
            }
            else
            {
                andereFarbe = Farbe.Weiss;
                gegenerKoenig = weisserKoenig;
            }
            if (gegenerKoenig.pos == new Position(-1, -1))
            {
                return true;
            }
            if (!wirdKoenigAngegriffen(andereFarbe))
            {
                return false;
            }
            //könig kann ins schach fahren
            /*if (gegenerKoenig.wasDarfFigur(figuren).Count > 0)
            {
                return false;
            }*/
            for (int x = 0; x <= 7; x++)
            {
                for (int y = 0; y <= 7; y++)
                {
                    if (figuren[x, y] != null && figuren[x, y].farbe != farbe)
                    {
                        foreach (Zug z in figuren[x, y].wasDarfFigur(figuren))
                        {
                            z.ausfueren(figuren);
                            if (!wirdKoenigAngegriffen(andereFarbe))
                            {
                                z.rueckgaengig(figuren);
                                return false;
                            }
                            z.rueckgaengig(figuren);
                        }
                    }
                }
            }
            return true;
        }
        public void bildaktualisieren()
        {

            for (int x = 0; x <= 7; x++)
            {
                for (int y = 0; y <= 7; y++)
                {
                    if (figuren[x, y] != null)
                    {
                        buttons[x, y].Image = Image.FromFile(figuren[x, y].bildpfad);
                    }
                    else
                    {
                        buttons[x, y].Image = null;
                    }
                }
            }
            if (!auswahlSchritt)
            {
                foreach (Zug z in ausgewaehlteFigur.wasDarfFigur(figuren))
                {
                    buttons[z.ziel.x, z.ziel.y].BackColor = Color.Green;
                }
            }
            else
            {
                for (int x = 0; x <= 7; x++)
                {
                    for (int y = 0; y <= 7; y++)
                    {
                        if ((x + y) % 2 == 0)
                        {
                            buttons[x, y].BackColor = Color.White;
                        }
                        else
                        {
                            buttons[x, y].BackColor = Color.Black;
                        }
                    }
                }
            }
        }
        private void feldClick(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Position buttonPosition = new Position(-1, -1);
            for (int x = 0; x <= 7; x++)
            {
                for (int y = 0; y <= 7; y++)
                {
                    if (b == buttons[x, y])
                    {
                        buttonPosition = new Position(x, y);
                    }
                }
            }
            //figur auswählen
            if (auswahlSchritt)
            {
                if (figuren[buttonPosition.x, buttonPosition.y] != null && figuren[buttonPosition.x, buttonPosition.y].farbe == spielendeFarbe)
                {
                    ausgewaehlteFigur = figuren[buttonPosition.x, buttonPosition.y];
                    auswahlSchritt = false;
                    bildaktualisieren();
                }
                return;
            }
            //figur bewegen
            else
            {
                auswahlSchritt = true;
                List<Zug> erlaubteZuege = ausgewaehlteFigur.wasDarfFigur(figuren);
                foreach (Zug z in erlaubteZuege)
                {
                    if (buttonPosition == z.ziel)
                    {
                        z.ausfueren(figuren);
                        gespielteZuege.Add(z);
                        bildaktualisieren();
                        if (hatFarbeGewonnen(spielendeFarbe))
                        {
                            if (spielendeFarbe == Farbe.Schwarz)
                            {
                                MessageBox.Show("Schwarz hat gewonnen");
                            }
                            else
                            {
                                MessageBox.Show("Weiss hat gewonnen");
                            }
                            System.Environment.Exit(0);
                        }
                        if (spielendeFarbe == Farbe.Schwarz)
                        {
                            spielendeFarbe = Farbe.Weiss;
                        }
                        else
                        {
                            spielendeFarbe = Farbe.Schwarz;
                        }
                        if (wirdKoenigAngegriffen(spielendeFarbe))
                        {
                            MessageBox.Show("Schach!!");
                        }
                        return;
                    }
                }
            }
            bildaktualisieren();
        }

        private void neuesSpiel()
        {
            Application.Restart();
        }
        private void neuesSpielToolStripMenuItem_Click(object sender, EventArgs e)
        {
            neuesSpiel();
        }
        private void spielBeenden()
        {
            Application.Exit();
        }
        private void spielbeendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spielBeenden();
        }

        private void zugRueckgaengig()
        {
            if(gespielteZuege.Count == 0)
            {
                MessageBox.Show("Alle Züge wurden rückgängig gemacht");
                return;
            }
            Zug z = gespielteZuege.Last();
            z.rueckgaengig(figuren);
            //löscht letzter zug
            gespielteZuege.RemoveAt(gespielteZuege.Count - 1);
            if (spielendeFarbe == Farbe.Schwarz)
            {
                spielendeFarbe = Farbe.Weiss;
            }
            else
            {
                spielendeFarbe = Farbe.Schwarz;
            }
            auswahlSchritt = true;
            bildaktualisieren();
        }
        private void rückgängigToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            zugRueckgaengig();
        }
        //hotkeys für menüpunkte
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                //schauen welche taste und event auslösen
                if (e.KeyCode == Keys.N)
                {
                    neuesSpiel();
                }
                else if (e.KeyCode == Keys.Z)
                {
                    zugRueckgaengig();
                }
                else if (e.KeyCode == Keys.E)
                {
                    spielBeenden();
                }
            }
        }
    }
}
