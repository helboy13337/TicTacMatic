using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TicTacMatic
{
    class againsabot
    {
        bool xbot;
        public againsabot(bool pXBot)
        {
            xbot = pXBot;
        }

        public void Play(player_vs_bot parent)
        {
            bool played = false;
            List<PictureBox> emptyButtons = new List<PictureBox>();
            foreach (PictureBox[] area in parent.fields)
            {
                emptyButtons.Clear();
                int counter = 0;
                int me = 0;
                foreach (PictureBox field in area)
                {
                    if (xbot && field.ImageLocation == "O.png") counter++;  // Sees opponent
                    else if (xbot && field.ImageLocation == "X.png") me++;  // Sees himself
                    else if (field.ImageLocation == null) emptyButtons.Add(field);  // Sees empty

                    if (!xbot && field.ImageLocation == "X.png") counter++;
                    else if (!xbot && field.ImageLocation == "O.png") me++;
                    else if (field.ImageLocation == null) emptyButtons.Add(field);
                }
                if (me == 2 && emptyButtons.Count != 0)
                {
                    //MessageBox.Show(emptyButtons[0].Name);
                    played = true;
                    parent.Play(emptyButtons[0], null);
                    return;
                }
                if (counter == 2 && emptyButtons.Count != 0)
                {
                    //MessageBox.Show(emptyButtons[0].Name);
                    played = true;
                    parent.Play(emptyButtons[0], null);
                    return;
                }
            }
            if (played == false) EmergencyPlay(parent);


            //if (moves.Count != 0)
            //{
            //    bool played = false;
            //    for (int i = 0; i < moves.Count; i++)
            //    {
            //        if (parent.GetField(moves[i]).ImageLocation == null)
            //        {
            //            played = true;
            //            parent.Play(parent.GetField(moves[i]), null);
            //            break;
            //        }
            //    }
            //
            //    // If no play could be made --> random
            //    if (!played) EmergencyPlay(parent);
            //}
            //else EmergencyPlay(parent);
        }

        private void EmergencyPlay(player_vs_bot parent)
        {
            List<int> emptyButtons = new List<int>();
            for (int i = 1; i < 10; i++)
            {
                if (parent.GetField(i).ImageLocation == null) emptyButtons.Add(i);
            }

            int index;
            try
            {
                index = emptyButtons[new Random().Next(0, emptyButtons.Count)];
            }

            catch (ArgumentOutOfRangeException)
            {  // If no more possible plays
                parent.ResetGame();
                parent.Close();
                return;
            }

            parent.Play(parent.GetField(index), null);
        }
    }
}