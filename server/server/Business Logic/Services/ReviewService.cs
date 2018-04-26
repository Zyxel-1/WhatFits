﻿using server.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using Whatfits.DataAccess.DataTransferObjects.CoreDTOs;
using Whatfits.DataAccess.DTOs.ContentDTOs;
using Whatfits.DataAccess.Gateways.ContentGateways;

namespace server.Services
{
    public class ReviewService
    {
        //Returns true or false on if review has been created, will be used with response
        public bool Create(ReviewsDTO rev)
        {
            var gateway = new ReviewsGateway();
            return gateway.AddReview(rev);
        }

        //Returns Review objects to front end, ReviewMessage, Rating, and Datetime
        public IEnumerable<ReviewDetailDTO> GetUserReview(UsernameDTO obj)
        {
            var gateway = new ReviewsGateway();
            return gateway.GetUserReviewDetails(obj);
        }
    }
}