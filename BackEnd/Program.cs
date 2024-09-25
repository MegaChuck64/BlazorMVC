
namespace BackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var backEndAddress = builder.Configuration["BackEndURL"];
            if (string.IsNullOrWhiteSpace(backEndAddress))
                throw new Exception("BackEndURL is missing from BackEnd config");

            var frontEndAddress = builder.Configuration["FrontEndURL"];
            if (string.IsNullOrWhiteSpace(frontEndAddress))
                throw new Exception("FrontEndURL is missing from BackEnd config");

            builder.Services.AddCors(
                options => options.AddDefaultPolicy(
                    policy => policy.WithOrigins(backEndAddress, frontEndAddress)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
            ));



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
