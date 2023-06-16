namespace WebApplication1.Sql
{
    public static class ClientQueries
    {
        public const string GetClient = @"
            SELECT
	            id as Id,
	            client_name as Name,
	            client_last_name as LastName,
	            email as Email,
	            phone as Phone
            FROM public.client 
        ";

        public static string AddClient = @"
            INSERT INTO public.client(
	            client_name, 
	            client_last_name, 
	            email,
	            phone)
            VALUES(
	            @ClientName,
	            @ClientLastName,
	            @Email,
	            @Phone)
            RETURNING id
	
        ";

		public static string UpdataClient = @"
		 UPDATE public.client
            SET 
                client_name = @ClientName,
                client_last_name = @ClientLastName,
                email = @Email,
                phone = @Phone
            WHERE id = @Id
            RETURNING id
		";

        public static string DeleteClient = @" 
            DELETE FROM public.client
            WHERE id = @id
            RETURNING 1
         ";
    }
}
