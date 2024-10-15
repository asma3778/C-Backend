using System;

namespace sda_3_online_Backend_Teamwork.src.Utils
{
    public class CustomException : Exception
    {
        public int StatusCode { get; set; }
        
        public CustomException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public static CustomException NotFound(string message = "Item not found")
        {
            return new CustomException(404, message);
        }

        public static CustomException BadRequest(string message = "Bad request")
        {
            return new CustomException(400, message);
        }

        public static CustomException UnAuthorized(string message = "Unauthorized. Please log in")
        {
            return new CustomException(401, message);
        }

        public static CustomException Forbidden(string message = "Forbidden. The user does not have access rights to the content")
        {
            return new CustomException(403, message);
        }

        public static CustomException InternalError(string message = "Internal server error")
        {
            return new CustomException(500, message);
        }

        public static CustomException Conflict(string message = "Conflict. The request could not be completed due to a conflict with the current state")
        {
            return new CustomException(409, message);
        }

        // Additional custom exceptions
        // change status num
        public static CustomException CreationFailed(string message = "Creation failed")
        {
            return new CustomException(501, message);
        }

        public static CustomException DeletionFailed(string message = "Deletion failed")
        {
            return new CustomException(502, message);
        }

        public static CustomException UpdateFailed(string message = "Update failed")
        {
            return new CustomException(503, message);
        }
    }
}
