using System.IO;

namespace Assets.Scripts.Game
{
    public class GameStateSaver
    {
        private const string playersFolderDir = "\\Players";

        public GameStateSaver() 
        { 
            
        }

        public void SavePlayer(Player playerToSave)
        {
            if (!File.Exists(playersFolderDir + $"{playerToSave.Name}.json"))
            {
                File.Create(playersFolderDir + $"{playerToSave.Name}.json");
            }

            File.WriteAllText(playersFolderDir + $"{playerToSave.Name}.json", playerToSave.ToString());
        }
    }
}
