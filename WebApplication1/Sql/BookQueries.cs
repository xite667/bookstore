namespace WebApplication1.Sql
{
    public static class BookQueries
    {
        public const string GetBooks = @"
            SELECT 
                id as Id,
                author_name as AuthorName,
                author_last_name as AuthorLastName,
                title as Title,
                year as Year
            FROM public.book";   
        

        public static string GetBookById = @$"
            {GetBooks}
            WHERE id = @id
        ";

        public static string CreateBook = $@"
            INSERT INTO public.book(
	            author_name, 
	            author_last_name, 
	            title,
	            year)
            VALUES(
	            @AuthorName,
	            @AuthorLastName,
	            @Title,
	            @Year)
            RETURNING id
        ";

        public static string UpdateBook = $@"
            UPDATE public.book
            SET 
                author_name = @AuthorName,
                author_last_name = @AuthorLastName,
                title = @Title,
                year = @Year
            WHERE id = @Id
            RETURNING id
        ";
        public static string DeleteBook = $@"
            DELETE FROM public.book
            WHERE id = @Id
            RETURNING 1
        ";
    }
}
