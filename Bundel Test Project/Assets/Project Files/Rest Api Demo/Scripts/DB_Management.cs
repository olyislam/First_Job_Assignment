using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


namespace RestAPI
{
    public class DB_Management : MonoBehaviour
    {
        protected readonly string uri = "https://5f2ed2fe64699b0016029280.mockapi.io/user_data";


        /// <summary>
        /// Get data from database
        /// </summary>
        /// <param name="userID">(OPTIONAL) if you need to get specific user data then pass to user id</param>
        /// <returns></returns>
        protected IEnumerator Get(string userID, Action<PlayerData> retrivedUser, Action<string> callbackMessage)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(this.uri + "/" + userID))
            {
                yield return www.SendWebRequest();
                if (www.isNetworkError || www.isHttpError)
                {
                    callbackMessage("Error Please try aganin");
                    yield break;
                }

                byte[] downloadedfile = www.downloadHandler.data;
                string json = System.Text.Encoding.UTF8.GetString(downloadedfile);
                PlayerData user = JsonUtility.FromJson<PlayerData>(json);


                retrivedUser(user);
                callbackMessage("Retrive a user success With Done");

            }
        }

        /// <summary>
        ///   /// Add new Data into the database
        /// </summary>
        /// <param name="jsonData">actual data that will upload to database</param>
        /// <param name="callbackMessage">call back information the process will colplete success or not</param>
        /// <returns></returns>
        protected IEnumerator Post(PlayerData user, Action<string> callbackMessage)
        {
            WWWForm form = new WWWForm();
            form.AddField("id", user.id);
            form.AddField("name", user.name);
            form.AddField("email", user.email);
            form.AddField("age", user.age);
            form.AddField("score", user.score);

            using (UnityWebRequest www = UnityWebRequest.Post(this.uri, form))
            {
                yield return www.SendWebRequest();
                if (www.isNetworkError || www.isHttpError)
                {
                    callbackMessage("Error Please try aganin");
                    yield break;
                }
                callbackMessage("Submission success With Done");

            }
        }

        /// <summary>
        /// update specific user data by user ID
        /// </summary>
        /// <param name="userID">that user data will be update in to the database</param>
        /// <param name="json">UpToDate user data</param>
        /// <returns></returns>
        protected IEnumerator Put(string userID, string json, Action<string> callbackMessage)
        {
            using (UnityWebRequest www = UnityWebRequest.Put(this.uri + "/" + userID, json))
            {
                yield return www.SendWebRequest();
                if (www.isNetworkError || www.isHttpError)
                {
                    callbackMessage("Error Please try aganin");
                    yield break;
                }

                callbackMessage("Update an user success With Done");

            }
        }

        /// <summary>
        /// delete specific user from database
        /// </summary>
        /// <param name="userID"> user id that will be delete from database </param>
        /// <returns></returns>
        protected IEnumerator Delete(string userID, Action<string> callbackMessage)
        {
            using (UnityWebRequest www = UnityWebRequest.Delete(this.uri + "/" + userID))
            {
                yield return www.SendWebRequest();
                if (www.isNetworkError || www.isHttpError)
                {
                    callbackMessage("Error Please try aganin");
                    yield break;
                }

                callbackMessage("Delete success With Done");

            }
        }
    }
}


