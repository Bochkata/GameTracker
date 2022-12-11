namespace GameTracker.Data
{
    public class DataConstants
    {
        public class BoardGameConstants
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 10;

            public const int CreatorNameMaxLength = 50;
            public const int CreatorNameMinLength = 5;

            public const string RatingMin = "0.00";
            public const string RatingMax = "10.00";
            public const string RatingDecimal = "decimal(18,2)";

            public const string InvalidBoardGameMessage = "You have entered invalid or incorrect data!";
            public const string InexistantType = "Type does not exist";
            public const string InvalidBoardGameId = "Invalid BoardGame Id";

        }
        public class BoardGameTypeConstants
        {
            public const int BoardGameTypeNameMaxLength = 50;
            public const int BoardGameTypeNameMinLength = 5;
        }
        public class BookGameConstants
        {
            public const int TitleMaxLength = 50;
            public const int TitleMinLength = 10;

            public const int WriterNameMaxLength = 50;
            public const int WriterNameMinLength = 5;

            public const string RatingMin = "0.00";
            public const string RatingMax = "10.00";
            public const string RatingDecimal = "decimal(18,2)";

            public const string InvalidBookGameMessage = "You have entered invalid or incorrect data!";
            public const string InexistantType = "Type does not exist";
            public const string InvalidBookGameId = "Invalid BookGame Id";

        }
        public class BookGameTypeConstants
        {
            public const int BookGameTypeNameMaxLength = 50;
            public const int BookGameTypeNameMinLength = 5;
        }
        public class ComputerGameConstants
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 10;

            public const int CreatorNameMaxLength = 50;
            public const int CreatorNameMinLength = 5;

            public const string RatingMin = "0.00";
            public const string RatingMax = "10.00";
            public const string RatingDecimal = "decimal(18,2)";

            public const string InvalidComputerGameMessage = "You have entered invalid or incorrect data!";
            public const string InexistantType = "Type does not exist";
            public const string InvalidComputerGameId = "Invalid ComputerGame Id";

        }
        public class ComputerGameTypeConstants
        {
            public const int ComputerGameTypeNameMaxLength = 50;
            public const int ComputerGameNameMinLength = 5;
        }
        public class ControllerConstants
        {
            public static string GeneralErrorMessage = "Something went wrong. Please try again!";
        }
        public class UserConstants
        {
            public const int UsernameMaxLength = 20;
            public const int UsernameMinLength = 5;

            public const int EmailMaxLength = 60;
            public const int EmailMinLength = 10;

            public const int PasswordMaxLength = 20;
            public const int PasswordMinLength = 5;

            public const string InvalidUserId = "Invalid User Id";
        }
        public static class RoleContants
        {
            public const string Admin = "Admin";

        }
    }
}
