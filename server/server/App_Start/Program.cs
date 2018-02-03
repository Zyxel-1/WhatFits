﻿using server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace server.App_Start
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new SchoolContext())
            {
                Student stud = new Student()
                {
                    StudentName = "New Student"
                };
                ctx.Students.Add(stud);
                ctx.SaveChanges();
            }
        }
    }
}