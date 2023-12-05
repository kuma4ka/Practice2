using Newtonsoft.Json;

namespace Practice_Linq
{
    public class Program
    {
        private static void Main(string[] args)
        {
            string path = @"../../../data/results_2010.json";

            List<FootballGame> games = ReadFromFileJson(path);

            int test_count = games.Count();
            Console.WriteLine($"Test value = {test_count}.");    // 13049

            Query1(games);
            Query2(games);
            Query3(games);
            Query4(games);
            Query5(games);
            Query6(games);
            Query7(games);
            Query8(games);
            Query9(games);
            Query10(games);
        }

        // Десеріалізація json-файлу у колекцію List<FootballGame>
        private static List<FootballGame> ReadFromFileJson(string path)
        {
            var reader = new StreamReader(path);
            string jsondata = reader.ReadToEnd();

            List<FootballGame> games = JsonConvert.DeserializeObject<List<FootballGame>>(jsondata);

            return games;
        }

        // Запит 1
        private static void Query1(List<FootballGame> games)
        {
            var selectedGames = games
                .Where(game => game.Country == "Ukraine" && game.Date.Year == 2012)
                .OrderBy(game => game.Date);

            Console.WriteLine("\n======================== QUERY 1 ========================");
            foreach (var game in selectedGames)
            {
                Console.WriteLine($"{game.Date:dd.MM.yyyy} {game.Home_team} - {game.Away_team}, Score: {game.Home_score} - {game.Away_score}, Country: {game.Country}");
            }
        }

        // Запит 2
        private static void Query2(List<FootballGame> games)
        {
            var selectedGames = games
                .Where(game => (game.Home_team == "Italy" || game.Away_team == "Italy") && game.Tournament == "Friendly" && game.Date.Year >= 2020)
                .OrderBy(game => game.Date);

            Console.WriteLine("\n======================== QUERY 2 ========================");
            foreach (var game in selectedGames)
            {
                Console.WriteLine($"{game.Date:dd.MM.yyyy} {game.Home_team} - {game.Away_team}, Score: {game.Home_score} - {game.Away_score}, Country: {game.Country}");
            }
        }

        // Запит 3
        private static void Query3(List<FootballGame> games)
        {
            var selectedGames = games
                .Where(game => (game.Home_team == "France" && game.Country == "France" && game.Date.Year == 2021 && game.Home_score == game.Away_score))
                .OrderBy(game => game.Date);

            Console.WriteLine("\n======================== QUERY 3 ========================");
            foreach (var game in selectedGames)
            {
                Console.WriteLine($"{game.Date:dd.MM.yyyy} {game.Home_team} - {game.Away_team}, Score: {game.Home_score} - {game.Away_score}, Country: {game.Country}");
            }
        }

        // Запит 4
        private static void Query4(List<FootballGame> games)
        {
            var selectedGames = games
                .Where(game => (game.Date.Year >= 2018 && game.Date.Year <= 2020 && game.Away_team == "Germany" && game.Home_score > game.Away_score))
                .OrderBy(game => game.Date);

            Console.WriteLine("\n======================== QUERY 4 ========================");
            foreach (var game in selectedGames)
            {
                Console.WriteLine($"{game.Date:dd.MM.yyyy} {game.Home_team} - {game.Away_team}, Score: {game.Home_score} - {game.Away_score}, Country: {game.Country}");
            }
        }

        // Запит 5
        private static void Query5(List<FootballGame> games)
        {
            var selectedGames = games
                .Where(game => (game.Tournament == "UEFA Euro qualification" && (game.City == "Kyiv" || game.City == "Kharkiv") && (game.Home_team == "Ukraine" && game.Home_score > game.Away_score)))
                .OrderBy(game => game.Date);

            Console.WriteLine("\n======================== QUERY 5 ========================");
            foreach (var game in selectedGames)
            {
                Console.WriteLine($"{game.Date:dd.MM.yyyy} {game.Home_team} - {game.Away_team}, Score: {game.Home_score} - {game.Away_score},  Country: {game.Country}");
            }
        }

        // Запит 6
        private static void Query6(List<FootballGame> games)
        {
            var selectedGames = games
                .Where(game => (game.Tournament == "FIFA World Cup"))
                .OrderByDescending(game => game.Date)
                .Take(8);

            Console.WriteLine("\n======================== QUERY 6 ========================");
            foreach (var game in selectedGames)
            {
                Console.WriteLine($"{game.Date:dd.MM.yyyy} {game.Home_team} - {game.Away_team}, Score: {game.Home_score} - {game.Away_score}, Country: {game.Country}");
            }
        }

        // Запит 7
        private static void Query7(List<FootballGame> games)
        {
            FootballGame? firstWinGame = games
                .FirstOrDefault(game => game.Date.Year == 2023 && (game.Home_team == "Ukraine" || game.Away_team == "Ukraine") && (game.Home_team == "Ukraine" && game.Home_score > game.Away_score || game.Away_team == "Ukraine" && game.Away_score > game.Home_score));

            Console.WriteLine("\n======================== QUERY 7 ========================");
            Console.WriteLine($"{firstWinGame.Date:dd.MM.yyyy} {firstWinGame.Home_team} - {firstWinGame.Away_team}, Score: {firstWinGame.Home_score} - {firstWinGame.Away_score}, Country: {firstWinGame.Country}");
        }

        // Запит 8
        private static void Query8(List<FootballGame> games)
        {
            var euro2012Matches = games
                .Where(game => game.Tournament == "UEFA Euro" && game.Country == "Ukraine")
                .Select(game => new
                {
                    MatchYear = game.Date.Year,
                    Team1 = game.Home_team,
                    Team2 = game.Away_team,
                    Goals = game.Home_score + game.Away_score
                });

            Console.WriteLine("\n======================== QUERY 8 ========================");
            foreach (var match in euro2012Matches)
            {
                Console.WriteLine($"{match.MatchYear} {match.Team1} - {match.Team2}, Goals: {match.Goals}");
            }
        }

        // Запит 9
        private static void Query9(List<FootballGame> games)
        {
            var uefaNationsLeagueMatches2023 = games
                .Where(game => game.Tournament == "UEFA Nations League" && game.Date.Year == 2023)
                .Select(game => new
                {
                    MatchYear = game.Date.Year,
                    Game = $"{game.Home_team} - {game.Away_team}",
                    Result = (game.Home_team == game.Away_team) ? "Draw" : (game.Home_score > game.Away_score) ? "Win" : (game.Home_score < game.Away_score) ? "Loss" : "Draw"
                });

            Console.WriteLine("\n======================== QUERY 9 ========================");
            foreach (var match in uefaNationsLeagueMatches2023)
            {
                Console.WriteLine($"{match.MatchYear} {match.Game}, Result for team1: {match.Result}");
            }
        }

        // Запит 10
        private static void Query10(List<FootballGame> games)
        {
            var goldCupMatchesJuly2023 = games
                .Where(game => game.Tournament == "Gold Cup" && game.Date.Year == 2023 && game.Date.Month == 7)
                .Skip(4)
                .Take(6);

            Console.WriteLine("\n======================== QUERY 10 ========================");

            foreach (var match in goldCupMatchesJuly2023)
            {
                Console.WriteLine($"{match.Date:dd.MM.yyyy} {match.Home_team} - {match.Away_team}, Score: {match.Home_score} - {match.Away_score}, Country: {match.Country}");
            }
        }
    }
}