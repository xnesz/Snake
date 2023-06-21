using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DBcon.Model;

namespace DBcon.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CRUDController : ControllerBase
    {

        private readonly MyContext db;
        public CRUDController(MyContext context)
        {
            db = context;
        }

        // POST: api/login
        // 200 OK - Login success
        // 400 Bad Request - Login failed
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] Login model)
        {
            try
            {
                // take username, password, api_key
                if (model.Username == null)
                {
                    throw new Exception("Missing username");
                }

                if (model.Password == null)
                {
                    throw new Exception("Missing password");
                }

                Login fetchedLogin = await db.Logins.FindAsync(model.Username);
                if (fetchedLogin == null)
                {
                    throw new Exception("User not registered");
                }

                if (!fetchedLogin.Password.Equals(model.Password))
                {
                    throw new Exception("Wrong password");
                }

                string sessionToken;
                Login login;

                do
                {
                    sessionToken = Guid.NewGuid().ToString();
                    login = await db.Logins.Where(l => l.SessionToken == sessionToken).FirstOrDefaultAsync();
                } while (login != null);

                fetchedLogin.SessionToken = sessionToken;
                await db.SaveChangesAsync();

                return Ok(fetchedLogin.SessionToken);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/register
        // 200 OK - User created
        // 400 Bad Request - Username already exists
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] Login model)
        {
            try
            {
                // take username, password, api_key
                if (model.Username == null)
                {
                    throw new Exception("Missing username");
                }

                if (model.Password == null)
                {
                    throw new Exception("Missing password");
                }

                Login fetchedLogin = await db.Logins.FindAsync(model.Username);
                if (fetchedLogin != null)
                {
                    throw new Exception("User already registered");
                }

                db.Logins.Add(model);
                await db.SaveChangesAsync();

                return Ok("User created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/highscore
        // 200 OK - Updated
        // 400 Bad Request - Something wrong
        [HttpPost("highscore")]
        public async Task<ActionResult<Highscore>> AddHighscore([FromBody] UpdateHighscoreModel model)
        {
            try
            {
                // take session_token, highscore, api_key
                if (model.SessionToken == null)
                {
                    throw new Exception("Missing session token");
                }

                Login login = await db.Logins.Where(l => l.SessionToken == model.SessionToken).FirstOrDefaultAsync();
                if(login == null)
                {
                    throw new Exception("Could not find matching session token");
                }

                Highscore existingHighscore = await db.Highscores.Where(h => h.Username == login.Username).FirstOrDefaultAsync();

                if(existingHighscore == null)
                {
                    db.Highscores.Add(new()
                    {
                        Username = login.Username,
                        Score = model.Score
                    });
                }
                else
                {
                    if(existingHighscore.Score < model.Score)
                    {
                        existingHighscore.Score = model.Score;
                    }
                }

                await db.SaveChangesAsync();

                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/highscore
        // 200 OK - List of highscores
        [HttpGet("highscore")]
        public async Task<ActionResult<IEnumerable<Highscore>>> GetHighscores()
        {
            //take api_key

            return await db.Highscores.OrderBy(h => -h.Score).Take(10).ToListAsync();
        }
    }
}