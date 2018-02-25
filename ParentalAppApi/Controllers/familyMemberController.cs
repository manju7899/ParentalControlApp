using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ParentalControlApi.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ParentalControlApi.Controllers
{


    public class familyMemberController : ApiController
    {
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        public List<FamilyMember> GetFamilyMembers()
            {
            List<FamilyMember> fMemberList = new List<FamilyMember>();
            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ParentalAppConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            DataSet ds =new DataSet(); 
            cmd.Connection = cn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GetAllFamilyMembers";
            SqlDataAdapter adp=new SqlDataAdapter(cmd);
            cn.Open();
            adp.Fill(ds);
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                FamilyMember member = new FamilyMember();
                member.id = Convert.ToInt32(dr["id"]);
                member.firstName = dr["firstname"] == DBNull.Value ? "" : dr["firstname"].ToString();
                member.lastName = dr["lastname"] == DBNull.Value ? "" : dr["lastname"].ToString();
                member.address = dr["adress"] == DBNull.Value ? "" : dr["adress"].ToString();
                member.mobileNumber = dr["mobilenumber"] == DBNull.Value ? "" : dr["mobilenumber"].ToString();
                member.gender = dr["gender"] == DBNull.Value ? "" : dr["gender"].ToString();
                fMemberList.Add(member);
            }
            return fMemberList;
        }


        public FamilyMember GetMember(int id)
        {
            FamilyMember member = new FamilyMember();
            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ParentalAppConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GetFamilyMember";
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cn.Open();
            adp.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //FamilyMember member = new FamilyMember();
                member.id = Convert.ToInt32(dr["id"]);
                member.firstName = dr["firstname"] == DBNull.Value ? "" : dr["firstname"].ToString();
                member.lastName = dr["lastname"] == DBNull.Value ? "" : dr["lastname"].ToString();
                member.address = dr["adress"] == DBNull.Value ? "" : dr["adress"].ToString();
                member.mobileNumber = dr["mobilenumber"] == DBNull.Value ? "" : dr["mobilenumber"].ToString();
                member.gender = dr["gender"] == DBNull.Value ? "" : dr["gender"].ToString();
            }
            return member;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        [HttpPost]
        public bool addMember([FromBody] FamilyMember familyMember)
        {
            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ParentalAppConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_AddFamilyMember";
            cmd.Parameters.AddWithValue("@firstname", familyMember.firstName);
            cmd.Parameters.AddWithValue("@lastname", familyMember.lastName);
            cmd.Parameters.AddWithValue("@adress", familyMember.address);
            cmd.Parameters.AddWithValue("@mobilenumber", familyMember.mobileNumber);
            cmd.Parameters.AddWithValue("@gender", familyMember.gender);
            cn.Open(); ZZZZZZZZ
           int effectedRows= cmd.ExecuteNonQuery();
           if (effectedRows > 0)
               return true;
           else
              return false;
            
        }

       [HttpPost]
        public bool updateMember([FromBody] FamilyMember familyMember)
        {
            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ParentalAppConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_UpdateFamilyMember";qweeeeeeeeeeeeeeeeeeeeeeeeetrkkkkkk3tr452225888888888888888888
            cmd.Parameters.AddWithValue("@id", familyMember.id);
            cmd.Parameters.AddWithValue("@firstname", familyMember.firstName);
            cmd.Parameters.AddWithValue("@lastname", familyMember.lastName);
            cmd.Parameters.AddWithValue("@adress", familyMember.address);
            cmd.Parameters.AddWithValue("@mobilenumber", familyMember.mobileNumber);
            cmd.Parameters.AddWithValue("@gender", familyMember.gender);
            cn.Open();
            int effectedRows = cmd.ExecuteNonQuery();
            if (effectedRows > 0)
                return true;
            else
                return false;
        }

        public bool deleteMember(int id)
        {
            SqlConnection cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ParentalAppConnection"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_DeleteFamilyMember";
            cmd.Parameters.AddWithValue("@id", id);
            cn.Open();
            int effectedRows = cmd.ExecuteNonQuery();
            if (effectedRows > 0)
                return true;
            else
                return false;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

    }
}
