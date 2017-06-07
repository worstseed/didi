using BattleConsoleApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace War
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Label[,] labels;
        private Battle battle;
        private List<BattleField[,]> prevAreas;
        private int actualArea;
        private int length;
        private int width;
        
        public MainWindow()
        {
            InitializeComponent();
            labels = new Label[10, 20];
            actualArea = 0;
            prevAreas = new List<BattleField[,]>();

            #region

            labels[0, 0] = Label0;
            labels[0, 1] = Label1;
            labels[0, 2] = Label2;
            labels[0, 3] = Label3;
            labels[0, 4] = Label4;
            labels[0, 5] = Label5;
            labels[0, 6] = Label6;
            labels[0, 7] = Label7;
            labels[0, 8] = Label8;
            labels[0, 9] = Label9;
            labels[0, 10] = Label10;
            labels[0, 11] = Label11;
            labels[0, 12] = Label12;
            labels[0, 13] = Label13;
            labels[0, 14] = Label14;
            labels[0, 15] = Label15;
            labels[0, 16] = Label16;
            labels[0, 17] = Label17;
            labels[0, 18] = Label18;
            labels[0, 19] = Label19;
            labels[1, 0] = Label20;
            labels[1, 1] = Label21;
            labels[1, 2] = Label22;
            labels[1, 3] = Label23;
            labels[1, 4] = Label24;
            labels[1, 5] = Label25;
            labels[1, 6] = Label26;
            labels[1, 7] = Label27;
            labels[1, 8] = Label28;
            labels[1, 9] = Label29;
            labels[1, 10] = Label30;
            labels[1, 11] = Label31;
            labels[1, 12] = Label32;
            labels[1, 13] = Label33;
            labels[1, 14] = Label34;
            labels[1, 15] = Label35;
            labels[1, 16] = Label36;
            labels[1, 17] = Label37;
            labels[1, 18] = Label38;
            labels[1, 19] = Label39;
            labels[2, 0] = Label40;
            labels[2, 1] = Label41;
            labels[2, 2] = Label42;
            labels[2, 3] = Label43;
            labels[2, 4] = Label44;
            labels[2, 5] = Label45;
            labels[2, 6] = Label46;
            labels[2, 7] = Label47;
            labels[2, 8] = Label48;
            labels[2, 9] = Label49;
            labels[2, 10] = Label50;
            labels[2, 11] = Label51;
            labels[2, 12] = Label52;
            labels[2, 13] = Label53;
            labels[2, 14] = Label54;
            labels[2, 15] = Label55;
            labels[2, 16] = Label56;
            labels[2, 17] = Label57;
            labels[2, 18] = Label58;
            labels[2, 19] = Label59;
            labels[3, 0] = Label60;
            labels[3, 1] = Label61;
            labels[3, 2] = Label62;
            labels[3, 3] = Label63;
            labels[3, 4] = Label64;
            labels[3, 5] = Label65;
            labels[3, 6] = Label66;
            labels[3, 7] = Label67;
            labels[3, 8] = Label68;
            labels[3, 9] = Label69;
            labels[3, 10] = Label70;
            labels[3, 11] = Label71;
            labels[3, 12] = Label72;
            labels[3, 13] = Label73;
            labels[3, 14] = Label74;
            labels[3, 15] = Label75;
            labels[3, 16] = Label76;
            labels[3, 17] = Label77;
            labels[3, 18] = Label78;
            labels[3, 19] = Label79;
            labels[4, 0] = Label80;
            labels[4, 1] = Label81;
            labels[4, 2] = Label82;
            labels[4, 3] = Label83;
            labels[4, 4] = Label84;
            labels[4, 5] = Label85;
            labels[4, 6] = Label86;
            labels[4, 7] = Label87;
            labels[4, 8] = Label88;
            labels[4, 9] = Label89;
            labels[4, 10] = Label90;
            labels[4, 11] = Label91;
            labels[4, 12] = Label92;
            labels[4, 13] = Label93;
            labels[4, 14] = Label94;
            labels[4, 15] = Label95;
            labels[4, 16] = Label96;
            labels[4, 17] = Label97;
            labels[4, 18] = Label98;
            labels[4, 19] = Label99;
            labels[5, 0] = Label100;
            labels[5, 1] = Label101;
            labels[5, 2] = Label102;
            labels[5, 3] = Label103;
            labels[5, 4] = Label104;
            labels[5, 5] = Label105;
            labels[5, 6] = Label106;
            labels[5, 7] = Label107;
            labels[5, 8] = Label108;
            labels[5, 9] = Label109;
            labels[5, 10] = Label110;
            labels[5, 11] = Label111;
            labels[5, 12] = Label112;
            labels[5, 13] = Label113;
            labels[5, 14] = Label114;
            labels[5, 15] = Label115;
            labels[5, 16] = Label116;
            labels[5, 17] = Label117;
            labels[5, 18] = Label118;
            labels[5, 19] = Label119;
            labels[6, 0] = Label120;
            labels[6, 1] = Label121;
            labels[6, 2] = Label122;
            labels[6, 3] = Label123;
            labels[6, 4] = Label124;
            labels[6, 5] = Label125;
            labels[6, 6] = Label126;
            labels[6, 7] = Label127;
            labels[6, 8] = Label128;
            labels[6, 9] = Label129;
            labels[6, 10] = Label130;
            labels[6, 11] = Label131;
            labels[6, 12] = Label132;
            labels[6, 13] = Label133;
            labels[6, 14] = Label134;
            labels[6, 15] = Label135;
            labels[6, 16] = Label136;
            labels[6, 17] = Label137;
            labels[6, 18] = Label138;
            labels[6, 19] = Label139;
            labels[7, 0] = Label140;
            labels[7, 1] = Label141;
            labels[7, 2] = Label142;
            labels[7, 3] = Label143;
            labels[7, 4] = Label144;
            labels[7, 5] = Label145;
            labels[7, 6] = Label146;
            labels[7, 7] = Label147;
            labels[7, 8] = Label148;
            labels[7, 9] = Label149;
            labels[7, 10] = Label150;
            labels[7, 11] = Label151;
            labels[7, 12] = Label152;
            labels[7, 13] = Label153;
            labels[7, 14] = Label154;
            labels[7, 15] = Label155;
            labels[7, 16] = Label156;
            labels[7, 17] = Label157;
            labels[7, 18] = Label158;
            labels[7, 19] = Label159;
            labels[8, 0] = Label160;
            labels[8, 1] = Label161;
            labels[8, 2] = Label162;
            labels[8, 3] = Label163;
            labels[8, 4] = Label164;
            labels[8, 5] = Label165;
            labels[8, 6] = Label166;
            labels[8, 7] = Label167;
            labels[8, 8] = Label168;
            labels[8, 9] = Label169;
            labels[8, 10] = Label170;
            labels[8, 11] = Label171;
            labels[8, 12] = Label172;
            labels[8, 13] = Label173;
            labels[8, 14] = Label174;
            labels[8, 15] = Label175;
            labels[8, 16] = Label176;
            labels[8, 17] = Label177;
            labels[8, 18] = Label178;
            labels[8, 19] = Label179;
            labels[9, 0] = Label180;
            labels[9, 1] = Label181;
            labels[9, 2] = Label182;
            labels[9, 3] = Label183;
            labels[9, 4] = Label184;
            labels[9, 5] = Label185;
            labels[9, 6] = Label186;
            labels[9, 7] = Label187;
            labels[9, 8] = Label188;
            labels[9, 9] = Label189;
            labels[9, 10] = Label190;
            labels[9, 11] = Label191;
            labels[9, 12] = Label192;
            labels[9, 13] = Label193;
            labels[9, 14] = Label194;
            labels[9, 15] = Label195;
            labels[9, 16] = Label196;
            labels[9, 17] = Label197;
            labels[9, 18] = Label198;
            labels[9, 19] = Label199;

            #endregion

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    labels[i, j].Background = Brushes.Black;
                    labels[i, j].Height = 24;
                    labels[i, j].Width = 24;
                }
            }

            battle = new Battle();
            length = battle.BattleArea.Length;
            width = battle.BattleArea.Width;
            //var numberOfRounds = 15;
            //for (var i = 1; i <= numberOfRounds; i++)
            //{
            //    //Display.WriteToConsole(battle.BattleArea);
            //    Display(battle.BattleArea);
            //    battle.PlayRound();
            //    //Console.WriteLine();
            //}
            //Console.ReadLine();

        }

        private void Display(BattleField[,] actualBattleArea)
        {
            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    BattleField field = actualBattleArea[i, j];
                    switch (field)
                    {
                        case BattleField.Walkable:
                            labels[i, j].Background = Brushes.LightGray;
                            break;
                        case BattleField.NotWalkable:
                            labels[i, j].Background = Brushes.Black;
                            break;
                        case BattleField.Roman:
                            labels[i, j].Background = Brushes.DarkRed;
                            break;
                        case BattleField.Gauls:
                            labels[i, j].Background = Brushes.ForestGreen;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        

        private void Button_Next(object sender, RoutedEventArgs e)
        {
            Display(battle.BattleArea.ActualBattleArea);
            if (actualArea >= prevAreas.Count())
            {
                BattleField[,] temp= new BattleField[10,20];
                temp = battle.BattleArea.ActualBattleArea;
                prevAreas.Add(temp);
            }
            actualArea++;
            battle.NextTurn();
        }

        private void Button_Previous(object sender, RoutedEventArgs e)
        {
            if (actualArea > 1 && prevAreas.Count() > 0)
            {
                actualArea--;
                Display(prevAreas[actualArea-1]);
            }
        }
    }
    
    
    
}
