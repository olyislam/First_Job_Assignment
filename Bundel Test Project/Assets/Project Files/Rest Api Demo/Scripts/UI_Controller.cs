using UnityEngine;
using UnityEngine.UI;

namespace RestAPI
{
    public class UI_Controller : DB_Management
    {
        #region Property
#pragma warning disable 649
        //user input fields. this property only use for Data Entry Side
        [Header("Data Entry Property")]
        [SerializeField] private InputField Name;
        [SerializeField] private InputField Email;
        [SerializeField] private InputField Age;
        [SerializeField] private InputField Score;
        [SerializeField] private InputField UserID;


        //this fild only use for data retrive side
        [Header("Data Retrive Property")]
        [SerializeField] private InputField _UserID;
        [SerializeField] private Text _Name;
        [SerializeField] private Text _Email;
        [SerializeField] private Text _Age;
        [SerializeField] private Text _Score;




        [Header("Button Property")]
        [SerializeField] private Button AddUser;
        [SerializeField] private Button UpdateUser;
        [SerializeField] private Button DeleteUser;
        [SerializeField] private Button RetriveUser;


        //it will show message like error or success informations
        [Header("Error or Success Message")]
        [SerializeField] private Text Message;
#pragma warning restore 649
        #endregion Property


        void Start()
        {
            //remove event listners
            AddUser.onClick.RemoveAllListeners();
            UpdateUser.onClick.RemoveAllListeners();
            DeleteUser.onClick.RemoveAllListeners();
            RetriveUser.onClick.RemoveAllListeners();

            //add 
            AddUser.onClick.AddListener(Add_User_);
            UpdateUser.onClick.AddListener(Update_User_);
            DeleteUser.onClick.AddListener(Delete_User_);
            RetriveUser.onClick.AddListener(Retrive_User);

        }


        private void Add_User_()
        {
            PlayerData user = CreateUser;
            string json = GetUserToJson(user);

            Refresh_DataEntry_Screen();//clear screen
            ShowMessage("Please wait for upload...");//show message to bottom on the screen
            
            
            StartCoroutine(Post(user, ShowMessage));//pass to database
        }
        private void Update_User_()
        {
            PlayerData user = CreateUser;
            string userid = this.UserID.text;
            user.id = userid;
            string json = GetUserToJson(user);

            Refresh_DataEntry_Screen();//clear screen
            ShowMessage("Please wait for update...");//show message to bottom on the screen

            StartCoroutine(Put(userid, json, ShowMessage));

        }
        private void Retrive_User()
        {
            string userid = this._UserID.text;
            userid = userid == "" ? "1" : userid;//if user missed input then it will show default user from DB


            Refresh_Retrive_Screen();//clear screen
            ShowMessage("Please wait for retrive...");//show message to bottom on the screen

            StartCoroutine(Get(userid, ShowRetrivedUser, ShowMessage));

        }
        private void Delete_User_()
        {
            string userid = this.UserID.text;

            Refresh_DataEntry_Screen();//clear screen
            ShowMessage("Please wait for delete...");//show message to bottom on the screen

            StartCoroutine(Delete(userid, ShowMessage));
        }




        /// <summary>
        /// Return A new "PlayerData" object
        /// </summary>
        private PlayerData CreateUser => new PlayerData(Name.text, Email.text, Age.text, Score.text);


        /// <summary>
        /// return jason data of a user  from type of "PlayerData"
        /// </summary>
        /// <param name="user">user data</param>
        /// <returns></returns>
        private string GetUserToJson(PlayerData user) => JsonUtility.ToJson(user);


        /// <summary>
        /// show user data on the screen into the data retrive side
        /// </summary>
        /// <param name="user"></param>
        private void ShowRetrivedUser(PlayerData user)
        {
            _Name.text = user.name;
            _Email.text = user.email;
            _Age.text = user.age;
            _Score.text = user.score;
        }
        private void ShowMessage(string message)
        {
            Message.text = message;
        }

        
        /// <summary>
        /// Cleare All of the datas from screen
        /// </summary>
        private void RefreshTotalScreen()
        {
            Refresh_DataEntry_Screen();
            Refresh_Retrive_Screen();
            Message.text = "";
        }

        /// <summary>
        /// cleare data entry side fields
        /// </summary>
        private void Refresh_DataEntry_Screen()
        {
            Name.text = "";
            Email.text = "";
            Age.text = "";
            Score.text = "";
            UserID.text = "";
        }
        /// <summary>
        ///  cleare data retrive side fields
        /// </summary>
        private void Refresh_Retrive_Screen()
        {
            _Name.text = "";
            _Email.text = "";
            _Age.text = "";
            _Score.text = "";
            _UserID.text = "";
        }
    }
}