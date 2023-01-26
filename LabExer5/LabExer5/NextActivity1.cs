using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace LabExer5
{
    [Activity(Label = "NextActivity1")]
    public class NextActivity1 : Activity
    {
        EditText editName, editSchool;
        Button btnAdd, btnSearch, btnUpdate, btnHome, btnDelete;
        RadioGroup gender;
        AutoCompleteTextView autoCompleteCountry;
        HttpWebResponse response;
        HttpWebRequest request;
        String name = "", school = "", country = "", selectedGender = "", res = "", str = "", login_name = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            // set layout
            SetContentView(Resource.Layout.next_layout);
            //get name of who login through Intent
            login_name = Intent.GetStringExtra("Name");

            //instantiate widgets
            editName = FindViewById<EditText>(Resource.Id.editText1);
            editSchool = FindViewById<EditText>(Resource.Id.editText2);
            btnAdd = FindViewById<Button>(Resource.Id.button1);
            btnSearch = FindViewById<Button>(Resource.Id.button2);
            btnUpdate = FindViewById<Button>(Resource.Id.button3);
            btnHome = FindViewById<Button>(Resource.Id.button4);    
            btnDelete = FindViewById<Button>(Resource.Id.button5);
            //set RadioGroup
            gender = FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            gender.CheckedChange += myRadioGroup_CheckedChange;
            //set AutoComplete
            autoCompleteCountry = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteTextView1);
            var country = new string[] { "Cambodia", "Indonesia", "Philippines", "Thailand", "Singapore" };
            ArrayAdapter adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, country);
            autoCompleteCountry.Adapter = adapter;

            btnAdd.Click += this.AddRecord;
            btnHome.Click += this.BackHome;
            btnSearch.Click += this.SearchRecord;
            btnUpdate.Click += this.UpdateRecord;
            btnDelete.Click += this.DeleteRecord;
        }
        void myRadioGroup_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            int checkedItemId = gender.CheckedRadioButtonId;
            RadioButton checkedRadioButton = FindViewById<RadioButton>(checkedItemId);
            selectedGender = checkedItemId.ToString();
            gender.Check(checkedItemId);
        }
        public void AddRecord(object sender, EventArgs e)
        {
            name = editName.Text;
            school = editSchool.Text;
            country = autoCompleteCountry.Text;

            request = (HttpWebRequest)WebRequest.Create("http://192.168.1.8/REST/add_record.php?name=" + name + " &school=" + school + " &country=" + country + " &gender=" + selectedGender);
            response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            res = reader.ReadToEnd();
            Toast.MakeText(this, res, ToastLength.Long).Show();
        }

        public void BackHome(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
        }
        public void UpdateRecord(object sender, EventArgs e)
        {
            name = editName.Text;
            school = editSchool.Text;
            country = autoCompleteCountry.Text;

            request = (HttpWebRequest)WebRequest.Create("http://192.168.1.8/REST/update_record.php?name=" + name + " &school=" + school + " &country=" + country + " &gender=" + selectedGender);
            response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            res = reader.ReadToEnd();
            Toast.MakeText(this, res, ToastLength.Long).Show();
        }
        public void DeleteRecord(object sender, EventArgs e)
        {
            name = editName.Text;
            school = editSchool.Text;
            country = autoCompleteCountry.Text;

            request = (HttpWebRequest)WebRequest.Create("http://192.168.1.8/REST/delete_record.php?name=" + name + " &school=" + school + " &country=" + country + " &gender=" + selectedGender);
            response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            res = reader.ReadToEnd();
            Toast.MakeText(this, res, ToastLength.Long).Show();
        }
        public void SearchRecord(object sender, EventArgs e)
        { 
            name = editName.Text;
            request = (HttpWebRequest)WebRequest.Create("http://192.168.1.8/REST/search_record.php?name=" + name);
            response = (HttpWebResponse)request.GetResponse();
            res = response.ProtocolVersion.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();
            //parse result to Json then kunin ang root element
            // not working not accessible
            using JsonDocument doc = JsonDocument.Parse(result);
            JsonElement root = doc.RootElement;

            var u1 = root[0]; //isa lang naman result ng query natin so 0 lang.kapag marami, mag loop kayo
            ///////////////////////////////////////////////
            //error PArse cannot be found
           
            //isa isahing kunin ang value na na-search
           
            string searchedname = u1.GetProperty("name").ToString();
            string searchedschool = u1.GetProperty("school").ToString();
            string searchedcountry = u1.GetProperty("country").ToString();
            int searchedgender = Convert.ToInt32(u1.GetProperty("gender").ToString());

            //i-set sa widget ang value na nasearch
            editName.Text = searchedname;
            editSchool.Text = searchedschool;
            autoCompleteCountry.Text = searchedcountry;
            gender.Check(searchedgender);
            Toast.MakeText(this, searchedgender.ToString(), ToastLength.Long).Show();
            
        }
    }
}