﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using Whatfits.Models.Models;
using Whatfits.Models.Context.Core;
using Whatfits.DataAccess.DataTransferObjects.CoreDTOs;
using Whatfits.DataAccess.GatewayInterfaces;

namespace Whatfits.DataAccess.Gateways.CoreGateways
{
    public class UserManagementGateway 
    {
        private UserManagementContext db = new UserManagementContext();
        public void CreateUser(UserManagementDTO obj)
        {
            // Creating new User
            User user = new User
            {
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Email = obj.Email,
                Gender = obj.Gender,
            };
            // Createing new Credential
            Credential credential = new Credential
            {
                UserName = obj.UserName,
                Password = obj.Password,
                IsFullyRegistered = obj.IsFullyRegistered,
                Status = obj.Status
            };
            // Creating new Salt
            Salt salt = new Salt
            {
                SaltValue = obj.Salt
            };
            // Save these tables since they are 1 to 1
            // So they'll have the same UserID
            db.Users.Add(user);
            db.Credentials.Add(credential);
            db.Salts.Add(salt);
            Save();
            // Find new User's ID
            var newUser = db.Credentials.Find(obj.UserName);
            // Creating location for User
            Location location = new Location
            {
                Address = obj.Address,
                City = obj.City,
                State = obj.State,
                Zipcode = obj.Zipcode,
                UserID = newUser.UserID
            };
            // Saving Data for new user
            db.Locations.Add(location);
            Save();
            // Add UserClaims
            for (int i = 0; i < obj.ClaimIDs.Count; i++)
            {
                UserClaims temp = new UserClaims { ClaimID = obj.ClaimIDs[i] };
                db.UserClaims.Add(temp);
                Save();
            }
            // Add Security QandAs
            for (int i = 0; i < obj.QuestionIDs.Count; i++)
            {
                SecurityQandA temp = new SecurityQandA { UserID = newUser.UserID, SecurityQuestionID = obj.QuestionIDs[i], Answer = obj.Answers[i] };
                db.SecurityQandA.Add(temp);
                Save();
            }
        }
        public void EditUser(UserManagementDTO obj)
        {
            // TODO: Need to do this but I need to understand 
            // what is being edited. Just UserName and Password?
        }
        public void DisableUser(UserManagementDTO obj)
        {
            var foundUser = db.Credentials.Find(obj.UserName);
            if (foundUser != null)
            {
                foundUser.Status = true;
                Save();
            }
        }
        public void EnableUser(UserManagementDTO obj)
        {
            var foundUser = db.Credentials.Find(obj.UserName);
            if (foundUser != null)
            {
                foundUser.Status = false;
                Save();
            }
        }
        public void DeleteUser(UserManagementDTO obj)
        {
            // Not Implementing
        }
        private void Save()
        {
            db.SaveChanges();
        }
    }
}
