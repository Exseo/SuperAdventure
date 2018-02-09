using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//use our logic code from Engine.css
using Engine;

namespace SuperAdventure
{ 

    public partial class SuperAdventure : Form
    {
        //creates private player variable to store our players stats
        private Player _player;

        public SuperAdventure()
        {
            InitializeComponent();

            //creates initial location object
            Location location = new Engine.Location(1, "Home", "This is your house.");
       
            //Instatiate a new player object
            _player = new Player(10, 10, 20, 0, 1);

            //assign properties above to text for ui
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblGold.Text = _player.CurrentHitPoints.ToString();
            lblExperience.Text = _player.ExperiencePoints.ToString();
            lblLevel.Text = _player.Level.ToString();
        

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SuperAdventure_Load(object sender, EventArgs e)
        {

        }

        private void SuperAdventure_Load_1(object sender, EventArgs e)
        {

        }

        

    }
}
