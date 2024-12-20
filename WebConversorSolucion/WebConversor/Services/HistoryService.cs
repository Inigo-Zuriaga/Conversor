namespace WebConversor.Services;

public class HistoryService
{
    private readonly IConfiguration _configuration; // Permite acceder a las configuraciones
    private readonly DbContexto _context; // Contexto de la bbdd
    
    public HistoryService(DbContexto context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;

    }

    // M�todo para crear un historial de intercambio basado en una solicitud de historial
    public async Task<string> CreateHistory(HistoryRequest history)
    {
        // Verifica si el usuario existe en la base de datos
        var userExist = _context.Users.FirstOrDefault(x => x.Email == history.Email);

        if(userExist == null)
        {
            return "No se puede generar el historial, el usuario no existe";
        }
        
        
        var countHistory = await _context.ExchangeHistory
            .Include(x => x.User)
            .Where(x => x.User.Email == history.Email)
            .OrderByDescending(x => x.Date)
            .ToListAsync();

        if (countHistory.Count == 10)
        {
            _context.ExchangeHistory.Remove(countHistory[9]);
        }
        // Crea un nuevo objeto de historial con los datos proporcionados
        var newHistory = new History
        {
            User = userExist,
            Date = history.Date,
            FromCoin = history.FromCoin,
            ToCoin = history.ToCoin,
            ToAmount = history.ToAmount,
            FromAmount = history.FromAmount,
        };
        
        _context.ExchangeHistory.Add(newHistory); // Agrega el nuevo historial al contexto
        await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos

        return "Historial creado con exito"; // Devuelve un mensaje de �xito
    }
    
    public async Task<bool> DeleteHistory(int id)
    {
        // Busca el historial por su id
        var history = _context.ExchangeHistory.FirstOrDefault(x => x.Id == id);

        if(history == null)
        {
            return false;
        }

        _context.ExchangeHistory.Remove(history); // Elimina el historial del contexto
        await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos

        // return "Historial eliminado con exito"; // Devuelve un mensaje de �xito
        return true;
    }

    public static string ToHtmlFile(List<HistoryRequest> data)
    {
        //string uploadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Exports");

// Crear el directorio si no existe
        //if (!Directory.Exists(uploadsPath))
        //{
        //    Directory.CreateDirectory(uploadsPath);
        //}
        
        // Guardar un archivo
        //string filePath = Path.Combine(uploadsPath, "test.pdf");
        // File.WriteAllText(filePath, "Contenido del archivo.");
        StringBuilder stringData=new StringBuilder(String.Empty);
        string tempHtml=String.Empty;
        try
        {
            string templatePath=Path.Combine(Directory.GetCurrentDirectory(), "HtmlTemplates", "historyPdf.html");
            //string templatePath=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HtmlTemplates", "historyPdf.html");
            tempHtml=File.ReadAllText(templatePath);
            // StringBuilder stringData=new StringBuilder(String.Empty);
            for(int i = 0; i < data.Count; i++)
            {
                stringData.Append($"<tr><td>{data[i].FromAmount} {data[i].FromCoin}</td><td>{data[i].ToAmount} {data[i].ToCoin}</td><td>{data[i].Date.ToString("yyyy-MM-dd HH:mm")}</td></tr>");

            };
            
        }
        catch (Exception e)
        {
            return e.Message;
        }
        //  string templatePath=Path.Combine(Directory.GetCurrentDirectory(), "HtmlTemplates", "historyPdf.html");
        // //string templatePath=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HtmlTemplates", "historyPdf.html");
        // string tempHtml=File.ReadAllText(templatePath);
        // // StringBuilder stringData=new StringBuilder(String.Empty);
        //     for(int i = 0; i < data.Count; i++)
        //     {
        //         stringData.Append($"<tr><td>{data[i].FromAmount} {data[i].FromCoin}</td><td>{data[i].ToAmount} {data[i].ToCoin}</td><td>{data[i].Date.ToString("yyyy-MM-dd HH:mm")}</td></tr>");
        //
        //     };
        return tempHtml.Replace("{data}", stringData.ToString());
    }
    
}