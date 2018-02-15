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

        //creates monster variable to hold current monster
        private Monster _currentMonster;

        public SuperAdventure()
        {
            InitializeComponent();

       
            //Instatiate a new player object
            _player = new Player(10, 10, 20, 0, 1);

            //sets player in starting location
            MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
            _player.Inventory.Add(new InventoryItem(
                World.ItemByID(World.ITEM_ID_RUSTY_SWORD), 1));
             

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

        private void btnNorth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToTheNorth);
        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToTheEast);
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToTheSouth);
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToTheWest);
        }

        private void MoveTo(Location newLocation)
        {
            //Does the location require items?
            if (newLocation.ItemRequiredToEnter != null)
            {
                //See if the player already has the item, default being false
                bool playerHasRequiredItem = false;

                foreach(InventoryItem ii in _player.Inventory)
                {
                    if(ii.Details.ID == newLocation.ItemRequiredToEnter.ID)
                    {
                        //found required item
                        bool playerHasRequiredItem = true;
                        break;
                    }
                }

                if (!playerHasRequiredItem)
                {
                    // We did not find the item in inventory
                    RichTextBoxMessages.Text += "You must have a " + newLocation.ItemRequiredToEnter.Name + " to enter this location." + Enviorment.NewLine;
                    return;
                }
            }

            // Update the players location
            _player.CurrentLocation = newLocation;

            //Show and Hide navigation buttons
            btnNorth.Visible = (newLocation.LocationToTheNorth != null);
            btnEast.Visible = (newLocation.LocationToTheEast != null);
            btnSouth.Visible = (newLocation.LocationToTheSouth != null);
            btnWest.Visible = (newLocation.LocationToTheWest != null);

            //Display current location name and description
            rtbLocation.Text = newLocation.Name + Environment.NewLine;
            rtbLocation.Text += newLocation.Description + Environment.NewLine;

            //

        }


        private void btnUseWeapon_Click(object sender, EventArgs e)
        {

        }

        private void btnUsePotion_Click(object sender, EventArgs e)
        {

        }
    }
}
