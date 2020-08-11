

namespace RestAPI
{
    public class PlayerData
    {
        public string id;
        public string name;
        public string email;
        public string age;
        public string score;

        
        public PlayerData() { }
        public PlayerData(string name, string email, string age, string score)
        {
            this.id = "";
            this.name = name;
            this.email = email;
            this.age = age;
            this.score = score;
        }
        
    }

}

   

