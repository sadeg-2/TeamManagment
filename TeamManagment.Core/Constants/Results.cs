namespace TeamManagment.Core.Constants
{
    public class Results
    {
        public static string AddSuccessResult()
        {
            return  "s: Add Element Successfully ";
        }

        public static string EditSuccessResult()
        {
            return "s:Item data has been updated successfully";
        }

        public static object UpdateStatusSuccessResult()
        {
            return "s:Status updated successfully";
        }

        public static string DeleteSuccessResult()
        {
            return "s:Item has been deleted successfully";
        }
        public static string AddFailResult()
        {
            return "e:Add operation failed";
        }

        public static string EditFailResult()
        {
            return "e:Failed to update item data";
        }

        public static string DeleteFailResult()
        {
            return "e:Item deletion failed";
        }
        public static string InputNotValid()
        {
            return "e:Check Again From Your Inputs :( ";
        }

        public static string RecoverFailResult()
        {
            return "e:Item Recovered failed";
        }
        public static string RecoverSuccessResult()
        {
            return "s:Recover Item Successfully";
        }

    }
}
