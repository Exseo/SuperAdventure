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
            dgvQuests.ClearSelection();
            dgvInventory.ClearSelection();

            //Does the location require items?
            if (!_player.HasRequiredItemToEnterThisLocation(newLocation))
            {
                rtbMessages.Text += "You must have a " + newLocation.ItemRequiredToEnter.Name + " to enter this location." + Environment.NewLine;
                return;
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

            // Completly heal player
            _player.CurrentHitPoints = _player.MaximumHitPoints;

            // Update Hit  Points in UI
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();

            // does the new location have any quests?
            if(newLocation.QuestAvailableHere != null)
            {
                //determin if the player has quet and if it is completed
                bool playerAlreadyHasQuest = _player.HasThisQuest(newLocation.QuestAvailableHere);
                bool playerAlreadyCompletedThisQuest = _player.CompletedThisQuest(newLocation.QuestAvailableHere);

                if (!playerAlreadyHasQuest)
                {

                    //The player does not have the quest

                    //Display the messages
                    rtbMessages.Text += "You recieve the " + newLocation.QuestAvailableHere.Name + "quest." + Environment.NewLine;
                    rtbMessages.Text += newLocation.QuestAvailableHere.Description + Environment.NewLine;
                    rtbMessages.Text += "To complete it, return with:" + Environment.NewLine;

                    foreach (QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompletionItems)
                    {
                        if (qci.Quantity == 1)
                        {
                            rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.Name + Environment.NewLine;
                        }
                        else
                        {
                            rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.NamePlural + Environment.NewLine;
                        }
                    }
                    rtbMessages.Text += Environment.NewLine;

                    //Add the Quest to the player's quest list
                    _player.Quests.Add(new PlayerQuest(newLocation.QuestAvailableHere));

                }

                // See if the player has the items needed to complete quest
                bool playerHasAllQuestItems = _player.HasAllQuestCompletionItems(newLocation.QuestAvailableHere);


                //Player has required items
                if (playerHasAllQuestItems)
                {
                    rtbMessages.Text += Environment.NewLine;
                    rtbMessages.Text += "You complete the " + newLocation.QuestAvailableHere.Name + " quest." + Environment.NewLine;

                    //remove quest items from an inventory
                    _player.RemoveQuestCompletionItems(newLocation.QuestAvailableHere);


                    // Give rewards!
                    rtbMessages.Text += "You recieve: " + Environment.NewLine;
                    rtbMessages.Text += newLocation.QuestAvailableHere.RewardExperiencePoints.ToString() + " experienece points" + Environment.NewLine;
                    rtbMessages.Text += newLocation.QuestAvailableHere.RewardGold.ToString() + " gold pieces" + Environment.NewLine;
                    rtbMessages.Text += newLocation.QuestAvailableHere.RewardItem.Name + Environment.NewLine;
                    rtbMessages.Text += Environment.NewLine;

                    _player.ExperiencePoints += newLocation.QuestAvailableHere.RewardExperiencePoints;
                    _player.Gold += newLocation.QuestAvailableHere.RewardGold;

                    //Add the reward item
                    _player.AddItemToInventory(newLocation.QuestAvailableHere.RewardItem);



                    //Mark the quest as completed
                    _player.MarkQuestCompleted(newLocation.QuestAvailableHere);
                }    
                
                

            }


            //Does our new location have a monster?
            if(newLocation.MonsterLivingHere != null)
            {
                rtbMessages.Text += "You see a " + newLocation.MonsterLivingHere.Name + Environment.NewLine;

                // Make a new Monster, using the values from the standard monster in the World.Monster list
                Monster standardMonster = World.MonsterByID(newLocation.MonsterLivingHere.ID);

                _currentMonster = new Monster(standardMonster.ID, standardMonster.Name, standardMonster.MaximumDamage,
                    standardMonster.RewardExperiencePoints, standardMonster.RewardGold, standardMonster.CurrentHitPoints,
                    standardMonster.MaximumHitPoints);

                foreach(LootItem lootItem in standardMonster.LootTable)
                {
                    _currentMonster.LootTable.Add(lootItem);
                }

                cboWeapons.Visible = true;
                cboPotions.Visible = true;
                btnUseWeapon.Visible = true;
                btnUsePotion.Visible = true;
            }
            //No monsters
            else
            {
                _currentMonster = null;

                cboWeapons.Visible = false;
                cboPotions.Visible = false;
                btnUseWeapon.Visible = false;
                btnUsePotion.Visible = false;
            }
            
            // Refresh player's inventory list
            UpdateInventoryListInUI();

            // Refresh player's quest list
            UpdateQuestListInUI();

            // Refresh player's weapons combobox
            UpdateWeaponListInUI();

            // Refresh player's potions combobox
            UpdatePotionListInUI();
        }

        private void UpdateInventoryListInUI()
        {
            dgvInventory.RowHeadersVisible = false;

            dgvInventory.ColumnCount = 2;
            dgvInventory.Columns[0].Name = "Name";
            dgvInventory.Columns[0].Width = 197;
            dgvInventory.Columns[1].Name = "Quantity";

            dgvInventory.Rows.Clear();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Quantity > 0)
                {
                    dgvInventory.Rows.Add(new[] {inventoryItem.Details.Name, inventoryItem.Quantity.ToString() });
                }

            }
        }

        private void UpdateQuestListInUI()
        {
            dgvQuests.RowHeadersVisible = false;

            dgvQuests.ColumnCount = 2;
            dgvQuests.Columns[0].Name = "Name";
            dgvQuests.Columns[0].Width = 197;
            dgvQuests.Columns[1].Name = "Done?";
            
            dgvQuests.Rows.Clear();


            foreach (PlayerQuest playerQuest in _player.Quests)
            {
                dgvQuests.Rows.Add(new[] {playerQuest.Details.Name, playerQuest.IsCompleted.ToString() });
            }
        }

        private void UpdateWeaponListInUI()
        {
            List<Weapon> weapons = new List<Weapon>();
            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is Weapon)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        weapons.Add((Weapon)inventoryItem.Details);
                    }
                }
            }
            if (weapons.Count == 0)
            {
                // The player doesn't have any weapons, so hide the weapon combobox and "Use" button
                cboWeapons.Visible = false;
                btnUseWeapon.Visible = false;
            }
            else
            {
                cboWeapons.DataSource = weapons;
                cboWeapons.DisplayMember = "Name";
                cboWeapons.ValueMember = "ID";
                cboWeapons.SelectedIndex = 0;
            }
        }

        private void UpdatePotionListInUI()
        {
            List<HealingPotion> healingPotions = new List<HealingPotion>();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is HealingPotion)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        healingPotions.Add(
                        (HealingPotion)inventoryItem.Details);
                    }
                }
            }

            if (healingPotions.Count == 0)
            {
                // The player doesn't have any potions, so hide the potion combobox and "Use" button
                cboPotions.Visible = false;
                btnUsePotion.Visible = false;
            }
            else
            {
                cboPotions.DataSource = healingPotions;
                cboPotions.DisplayMember = "Name";
                cboPotions.ValueMember = "ID";
                cboPotions.SelectedIndex = 0;
            }
        }

        private void btnUseWeapon_Click(object sender, EventArgs e)
        {

        }

        private void btnUsePotion_Click(object sender, EventArgs e)
        {

        }

    }
}
