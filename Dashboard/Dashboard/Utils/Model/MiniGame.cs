namespace Dashboard.Utils.Model
{
    public class MiniGame
    {
        private int idGame;
        private string name;
        private int time;
        private int points;
        private bool isBoss;

        public int IdGame
        {
            get { return idGame; }
            set { idGame = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Time
        {
            get { return time; }
            set { time = value; }
        }

        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        public bool IsBoss
        {
            get { return isBoss; }
            set { isBoss = value; }
        }

        public MiniGame(int idGame, string name, int time, int points, bool isBoss)
        {
            this.idGame = idGame;
            this.name = name;
            this.time = time;
            this.points = points;
            this.isBoss = isBoss;
        }
        public MiniGame()
        {
        }
    }
}
