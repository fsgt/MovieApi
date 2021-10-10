using System;
using Demo.Movies.TheMovieDb.Abstractions.Services;
using Demo.Movies.TheMovieDb.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using Xunit;

namespace Demo.Movies.TheMovieDb.Tests
{
    public class MovieMapperTests
    {
        private readonly IServiceProvider _provider;

        public MovieMapperTests()
        {
            var services = new ServiceCollection();
            services.AddTransient<IMovieMapper, MovieMapper>();
            _provider = services.BuildServiceProvider();
        }

        [Theory]
        [InlineData("NowPlaying")]
        [InlineData("TopRated")]
        [InlineData("Upcoming")]
        public void JsonMock(string data)
        {
            var mapper = _provider.GetRequiredService<IMovieMapper>();

            var input = data switch
            {
                "NowPlaying" => JsonConvert.DeserializeObject<SearchContainerWithDates<SearchMovie>>(_nowPlayingJson),
                "TopRated" => JsonConvert.DeserializeObject<SearchContainer<SearchMovie>>(_topRatedJson),
                "Upcoming" => JsonConvert.DeserializeObject<SearchContainerWithDates<SearchMovie>>(_upcomingJson),
                _ => throw new NotImplementedException(),
            };

            var result = mapper.Map(input);

            for (int i = 0; i < result.Count; i++)
            {
                Assert.Equal(input.Results[i].Title, result[i].Title, StringComparer.Ordinal);
            }

            Assert.Equal(input.Results.Count, result.Count);
        }

        [Fact]
        public void CoalesceNull()
        {
            var mapper = _provider.GetRequiredService<IMovieMapper>();
            var result = mapper.Map(null);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void CoalesceNullItem()
        {
            var mapper = _provider.GetRequiredService<IMovieMapper>();
            var input = JsonConvert.DeserializeObject<SearchContainerWithDates<SearchMovie>>(_upcomingJson);
            input.Results[3] = null;
            var result = mapper.Map(input);

            for (int i = 0; i < result.Count; i++)
            {
                Assert.Equal(input.Results[i >= 3 ? i + 1 : i].Title, result[i].Title, StringComparer.Ordinal);
            }

            Assert.Equal(input.Results.Count - 1, result.Count);
        }



        private const string _nowPlayingJson = @"{
  ""dates"": {
    ""maximum"": ""2021-10-15T00:00:00"",
    ""minimum"": ""2021-08-28T00:00:00""
  },
  ""page"": 1,
  ""results"": [
    {
      ""adult"": false,
      ""originalTitle"": ""Venom: Let There Be Carnage"",
      ""releaseDate"": ""2021-09-30T00:00:00"",
      ""title"": ""Venom: Let There Be Carnage"",
      ""video"": false,
      ""backdropPath"": ""/t9nyF3r0WAlJ7Kr6xcRYI4jr9jm.jpg"",
      ""genreIds"": [878, 28],
      ""originalLanguage"": ""en"",
      ""overview"": ""After finding a host body in investigative reporter Eddie Brock, the alien symbiote must face a new enemy, Carnage, the alter ego of serial killer Cletus Kasady."",
      ""posterPath"": ""/rjkmN1dniUHVYAtwuV3Tji7FsDO.jpg"",
      ""voteAverage"": 7.5,
      ""voteCount"": 346,
      ""id"": 580489,
      ""mediaType"": 1,
      ""popularity"": 11173.275
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Free Guy"",
      ""releaseDate"": ""2021-08-11T00:00:00"",
      ""title"": ""Free Guy"",
      ""video"": false,
      ""backdropPath"": ""/8Y43POKjjKDGI9MH89NW0NAzzp8.jpg"",
      ""genreIds"": [35, 28, 12, 878],
      ""originalLanguage"": ""en"",
      ""overview"": ""A bank teller called Guy realizes he is a background character in an open world video game called Free City that will soon go offline."",
      ""posterPath"": ""/xmbU4JTUm8rsdtn7Y3Fcm30GpeT.jpg"",
      ""voteAverage"": 7.9,
      ""voteCount"": 2393,
      ""id"": 550988,
      ""mediaType"": 1,
      ""popularity"": 8075.543
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Snake Eyes: G.I. Joe Origins"",
      ""releaseDate"": ""2021-07-22T00:00:00"",
      ""title"": ""Snake Eyes: G.I. Joe Origins"",
      ""video"": false,
      ""backdropPath"": ""/aO9Nnv9GdwiPdkNO79TISlQ5bbG.jpg"",
      ""genreIds"": [28, 12],
      ""originalLanguage"": ""en"",
      ""overview"": ""After saving the life of their heir apparent, tenacious loner Snake Eyes is welcomed into an ancient Japanese clan called the Arashikage where he is taught the ways of the ninja warrior. But, when secrets from his past are revealed, Snake Eyes' honor and allegiance will be tested – even if that means losing the trust of those closest to him."",
      ""posterPath"": ""/uIXF0sQGXOxQhbaEaKOi2VYlIL0.jpg"",
      ""voteAverage"": 6.9,
      ""voteCount"": 602,
      ""id"": 568620,
      ""mediaType"": 1,
      ""popularity"": 4019.003
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Shang-Chi and the Legend of the Ten Rings"",
      ""releaseDate"": ""2021-09-01T00:00:00"",
      ""title"": ""Shang-Chi and the Legend of the Ten Rings"",
      ""video"": false,
      ""backdropPath"": ""/cinER0ESG0eJ49kXlExM0MEWGxW.jpg"",
      ""genreIds"": [28, 12, 14],
      ""originalLanguage"": ""en"",
      ""overview"": ""Shang-Chi must confront the past he thought he left behind when he is drawn into the web of the mysterious Ten Rings organization."",
      ""posterPath"": ""/1BIoJGKbXjdFDAqUEiA2VHqkK1Z.jpg"",
      ""voteAverage"": 7.8,
      ""voteCount"": 1227,
      ""id"": 566525,
      ""mediaType"": 1,
      ""popularity"": 1699.345
    },
    {
      ""adult"": false,
      ""originalTitle"": ""The Addams Family 2"",
      ""releaseDate"": ""2021-10-01T00:00:00"",
      ""title"": ""The Addams Family 2"",
      ""video"": false,
      ""backdropPath"": ""/kTOheVmqSBDIRGrQLv2SiSc89os.jpg"",
      ""genreIds"": [16, 35, 10751],
      ""originalLanguage"": ""en"",
      ""overview"": ""The Addams get tangled up in more wacky adventures and find themselves involved in hilarious run-ins with all sorts of unsuspecting characters."",
      ""posterPath"": ""/xYLBgw7dHyEqmcrSk2Sq3asuSq5.jpg"",
      ""voteAverage"": 7.8,
      ""voteCount"": 189,
      ""id"": 639721,
      ""mediaType"": 1,
      ""popularity"": 2112.639
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Solitary"",
      ""releaseDate"": ""2021-09-24T00:00:00"",
      ""title"": ""Solitary"",
      ""video"": false,
      ""backdropPath"": ""/iDLtDgxLiYsarfdQ4msUhUqoNPp.jpg"",
      ""genreIds"": [878],
      ""originalLanguage"": ""en"",
      ""overview"": ""A man wakes up inside a room to discover he's a prisoner sent into space to form Earth's first colony, and worse - his cell mate Alana is hell bent on destroying everything."",
      ""posterPath"": ""/ApwmbrMlsuOay5rXQA4Kbz7qJAl.jpg"",
      ""voteAverage"": 6.6,
      ""voteCount"": 32,
      ""id"": 725273,
      ""mediaType"": 1,
      ""popularity"": 1596.076
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Jungle Cruise"",
      ""releaseDate"": ""2021-07-28T00:00:00"",
      ""title"": ""Jungle Cruise"",
      ""video"": false,
      ""backdropPath"": ""/7WJjFviFBffEJvkAms4uWwbcVUk.jpg"",
      ""genreIds"": [12, 14, 35, 28],
      ""originalLanguage"": ""en"",
      ""overview"": ""Dr. Lily Houghton enlists the aid of wisecracking skipper Frank Wolff to take her down the Amazon in his dilapidated boat. Together, they search for an ancient tree that holds the power to heal – a discovery that will change the future of medicine."",
      ""posterPath"": ""/9dKCd55IuTT5QRs989m9Qlb7d2B.jpg"",
      ""voteAverage"": 7.8,
      ""voteCount"": 2884,
      ""id"": 451048,
      ""mediaType"": 1,
      ""popularity"": 1327.033
    },
    {
      ""adult"": false,
      ""originalTitle"": ""F9"",
      ""releaseDate"": ""2021-05-19T00:00:00"",
      ""title"": ""F9"",
      ""video"": false,
      ""backdropPath"": ""/aT0XL7YLDx9GfpU2q8kgWUtn0on.jpg"",
      ""genreIds"": [28, 80, 53],
      ""originalLanguage"": ""en"",
      ""overview"": ""Dominic Toretto and his crew battle the most skilled assassin and high-performance driver they've ever encountered: his forsaken brother."",
      ""posterPath"": ""/bOFaAXmWWXC3Rbv4u4uM9ZSzRXP.jpg"",
      ""voteAverage"": 7.5,
      ""voteCount"": 3982,
      ""id"": 385128,
      ""mediaType"": 1,
      ""popularity"": 1262.729
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Ainbo: Spirit of the Amazon"",
      ""releaseDate"": ""2021-02-09T00:00:00"",
      ""title"": ""Ainbo: Spirit of the Amazon"",
      ""video"": false,
      ""backdropPath"": ""/3GgkzCDq6KYpcmJmcOKh27hYRyj.jpg"",
      ""genreIds"": [12, 16, 10751, 14],
      ""originalLanguage"": ""en"",
      ""overview"": ""An epic journey of a young hero and her Spirit Guides, 'Dillo' a cute and humorous armadillo and \""Vaca\"" a goofy oversized tapir, who embark on a quest to save their home in the spectacular Amazon Rainforest."",
      ""posterPath"": ""/l8HyObVj8fPrzacAPtGWWLDhcfh.jpg"",
      ""voteAverage"": 7.2,
      ""voteCount"": 189,
      ""id"": 588921,
      ""mediaType"": 1,
      ""popularity"": 1086.405
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Kate"",
      ""releaseDate"": ""2021-09-10T00:00:00"",
      ""title"": ""Kate"",
      ""video"": false,
      ""backdropPath"": ""/byflnwPMumyvrCW9SfO5Miq3647.jpg"",
      ""genreIds"": [28, 53],
      ""originalLanguage"": ""en"",
      ""overview"": ""After she's irreversibly poisoned, a ruthless criminal operative has less than 24 hours to exact revenge on her enemies and in the process forms an unexpected bond with the daughter of one of her past victims."",
      ""posterPath"": ""/uJgdT1boTSP0dDIjdTgGleg71l4.jpg"",
      ""voteAverage"": 6.8,
      ""voteCount"": 612,
      ""id"": 597891,
      ""mediaType"": 1,
      ""popularity"": 960.986
    },
    {
      ""adult"": false,
      ""originalTitle"": ""The Boss Baby: Family Business"",
      ""releaseDate"": ""2021-07-01T00:00:00"",
      ""title"": ""The Boss Baby: Family Business"",
      ""video"": false,
      ""backdropPath"": ""/akwg1s7hV5ljeSYFfkw7hTHjVqk.jpg"",
      ""genreIds"": [16, 35, 12, 10751],
      ""originalLanguage"": ""en"",
      ""overview"": ""The Templeton brothers — Tim and his Boss Baby little bro Ted — have become adults and drifted away from each other. But a new boss baby with a cutting-edge approach and a can-do attitude is about to bring them together again … and inspire a new family business."",
      ""posterPath"": ""/uWStkK8bq9ixY3fc7y209ZleCoF.jpg"",
      ""voteAverage"": 7.7,
      ""voteCount"": 1481,
      ""id"": 459151,
      ""mediaType"": 1,
      ""popularity"": 1083.327
    },
    {
      ""adult"": false,
      ""originalTitle"": ""PAW Patrol: The Movie"",
      ""releaseDate"": ""2021-08-09T00:00:00"",
      ""title"": ""PAW Patrol: The Movie"",
      ""video"": false,
      ""backdropPath"": ""/mtRW6eAwOO27h3zrfU2foQFW7Hg.jpg"",
      ""genreIds"": [16, 10751, 12, 35],
      ""originalLanguage"": ""en"",
      ""overview"": ""Ryder and the pups are called to Adventure City to stop Mayor Humdinger from turning the bustling metropolis into a state of chaos."",
      ""posterPath"": ""/ic0intvXZSfBlYPIvWXpU1ivUCO.jpg"",
      ""voteAverage"": 7.8,
      ""voteCount"": 579,
      ""id"": 675445,
      ""mediaType"": 1,
      ""popularity"": 1030.178
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Catch the Bullet"",
      ""releaseDate"": ""2021-09-10T00:00:00"",
      ""title"": ""Catch the Bullet"",
      ""video"": false,
      ""backdropPath"": ""/MDYanFolbT76dj0gsCbhS2GM5A.jpg"",
      ""genreIds"": [37, 28],
      ""originalLanguage"": ""en"",
      ""overview"": ""U.S. marshal Britt MacMasters returns from a mission to find his father wounded and his son kidnapped by the outlaw Jed Blake. Hot on their trail, Britt forms a posse with a gunslinging deputy and a stoic Pawnee tracker. But Jed and Britt tread dangerously close to the Red Desert’s Sioux territory."",
      ""posterPath"": ""/7PoomidF9HlMKXcAyOJ87lGkhSp.jpg"",
      ""voteAverage"": 5.6,
      ""voteCount"": 53,
      ""id"": 859860,
      ""mediaType"": 1,
      ""popularity"": 1077.195
    },
    {
      ""adult"": false,
      ""originalTitle"": ""My Little Pony: A New Generation"",
      ""releaseDate"": ""2021-09-23T00:00:00"",
      ""title"": ""My Little Pony: A New Generation"",
      ""video"": false,
      ""backdropPath"": ""/ugukqzx4gSzBd1yzmbWEHLkpGaS.jpg"",
      ""genreIds"": [16, 10751, 14, 35, 10402, 12],
      ""originalLanguage"": ""en"",
      ""overview"": ""Equestria's divided. But a bright-eyed hero believes Earth Ponies, Pegasi and Unicorns should be pals — and, hoof to heart, she’s determined to prove it."",
      ""posterPath"": ""/hzq5XRGgm6NDMOW1idUvbpGqEkv.jpg"",
      ""voteAverage"": 8,
      ""voteCount"": 88,
      ""id"": 597316,
      ""mediaType"": 1,
      ""popularity"": 1025.992
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Space Jam: A New Legacy"",
      ""releaseDate"": ""2021-07-08T00:00:00"",
      ""title"": ""Space Jam: A New Legacy"",
      ""video"": false,
      ""backdropPath"": ""/8s4h9friP6Ci3adRGahHARVd76E.jpg"",
      ""genreIds"": [16, 35, 10751, 878],
      ""originalLanguage"": ""en"",
      ""overview"": ""When LeBron and his young son Dom are trapped in a digital space by a rogue A.I., LeBron must get them home safe by leading Bugs, Lola Bunny and the whole gang of notoriously undisciplined Looney Tunes to victory over the A.I.'s digitized champions on the court. It's Tunes versus Goons in the highest-stakes challenge of his life."",
      ""posterPath"": ""/5bFK5d3mVTAvBCXi5NPWH0tYjKl.jpg"",
      ""voteAverage"": 7.3,
      ""voteCount"": 2377,
      ""id"": 379686,
      ""mediaType"": 1,
      ""popularity"": 941.915
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Don't Breathe 2"",
      ""releaseDate"": ""2021-08-12T00:00:00"",
      ""title"": ""Don't Breathe 2"",
      ""video"": false,
      ""backdropPath"": ""/pUc51UUQb1lMLVVkDCaZVsCo37U.jpg"",
      ""genreIds"": [53, 27],
      ""originalLanguage"": ""en"",
      ""overview"": ""The Blind Man has been hiding out for several years in an isolated cabin and has taken in and raised a young girl orphaned from a devastating house fire. Their quiet life together is shattered when a group of criminals kidnap the girl, forcing the Blind Man to leave his safe haven to save her."",
      ""posterPath"": ""/hRMfgGFRAZIlvwVWy8DYJdLTpvN.jpg"",
      ""voteAverage"": 7.7,
      ""voteCount"": 917,
      ""id"": 482373,
      ""mediaType"": 1,
      ""popularity"": 885.699
    },
    {
      ""adult"": false,
      ""originalTitle"": ""After We Fell"",
      ""releaseDate"": ""2021-09-01T00:00:00"",
      ""title"": ""After We Fell"",
      ""video"": false,
      ""backdropPath"": ""/qD45xHA35HdJDGOaA1AgDwiWEgO.jpg"",
      ""genreIds"": [10749, 18],
      ""originalLanguage"": ""en"",
      ""overview"": ""Just as Tessa's life begins to become unglued, nothing is what she thought it would be. Not her friends nor her family. The only person that she should be able to rely on is Hardin, who is furious when he discovers the massive secret that she's been keeping. Before Tessa makes the biggest decision of her life, everything changes because of revelations about her family."",
      ""posterPath"": ""/3WfvjNWr5k1Zzww81b3GWc8KQhb.jpg"",
      ""voteAverage"": 8.1,
      ""voteCount"": 325,
      ""id"": 744275,
      ""mediaType"": 1,
      ""popularity"": 772.466
    },
    {
      ""adult"": false,
      ""originalTitle"": ""No Time to Die"",
      ""releaseDate"": ""2021-09-29T00:00:00"",
      ""title"": ""No Time to Die"",
      ""video"": false,
      ""backdropPath"": ""/u5Fp9GBy9W8fqkuGfLBuuoJf57Z.jpg"",
      ""genreIds"": [12, 28, 53],
      ""originalLanguage"": ""en"",
      ""overview"": ""Bond has left active service and is enjoying a tranquil life in Jamaica. His peace is short-lived when his old friend Felix Leiter from the CIA turns up asking for help. The mission to rescue a kidnapped scientist turns out to be far more treacherous than expected, leading Bond onto the trail of a mysterious villain armed with dangerous new technology."",
      ""posterPath"": ""/iUgygt3fscRoKWCV1d0C7FbM9TP.jpg"",
      ""voteAverage"": 7.4,
      ""voteCount"": 460,
      ""id"": 370172,
      ""mediaType"": 1,
      ""popularity"": 779.673
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Malignant"",
      ""releaseDate"": ""2021-09-01T00:00:00"",
      ""title"": ""Malignant"",
      ""video"": false,
      ""backdropPath"": ""/xDnFlNrNUoSKPq4uptnhYmUZNpm.jpg"",
      ""genreIds"": [27, 53, 9648],
      ""originalLanguage"": ""en"",
      ""overview"": ""Madison is paralyzed by shocking visions of grisly murders, and her torment worsens as she discovers that these waking dreams are in fact terrifying realities with a mysterious tie to her past."",
      ""posterPath"": ""/dGv2BWjzwAz6LB8a8JeRIZL8hSz.jpg"",
      ""voteAverage"": 7.1,
      ""voteCount"": 737,
      ""id"": 619778,
      ""mediaType"": 1,
      ""popularity"": 656.433
    },
    {
      ""adult"": false,
      ""originalTitle"": ""The Courier"",
      ""releaseDate"": ""2021-03-18T00:00:00"",
      ""title"": ""The Courier"",
      ""video"": false,
      ""backdropPath"": ""/3pIqd1hgZ2xqzWEyiYp4blqE9Fi.jpg"",
      ""genreIds"": [53, 36, 18],
      ""originalLanguage"": ""en"",
      ""overview"": ""Cold War spy Greville Wynne and his Russian source try to put an end to the Cuban Missile Crisis."",
      ""posterPath"": ""/zFIjKtZrzhmc7HecdFXXjsLR2Ig.jpg"",
      ""voteAverage"": 7.1,
      ""voteCount"": 413,
      ""id"": 522241,
      ""mediaType"": 1,
      ""popularity"": 646.757
    }
  ],
  ""totalPages"": 83,
  ""totalResults"": 1656
}
";

        private const string _topRatedJson = @"{
  ""page"": 1,
  ""results"": [
    {
      ""adult"": false,
      ""originalTitle"": ""??????? ????????? ?? ???????"",
      ""releaseDate"": ""1995-10-20T00:00:00"",
      ""title"": ""Dilwale Dulhania Le Jayenge"",
      ""video"": false,
      ""backdropPath"": ""/5hNcsnMkwU2LknLoru73c76el3z.jpg"",
      ""genreIds"": [
        35,
        18,
        10749
      ],
      ""originalLanguage"": ""hi"",
      ""overview"": ""Raj is a rich, carefree, happy-go-lucky second generation NRI. Simran is the daughter of Chaudhary Baldev Singh, who in spite of being an NRI is very strict about adherence to Indian values. Simran has left for India to be married to her childhood fiancé. Raj leaves for India with a mission at his hands, to claim his lady love under the noses of her whole family. Thus begins a saga."",
      ""posterPath"": ""/2CAL2433ZeIihfX1Hb2139CX0pW.jpg"",
      ""voteAverage"": 8.8,
      ""voteCount"": 3167,
      ""id"": 19404,
      ""mediaType"": 1,
      ""popularity"": 23.147
    },
    {
      ""adult"": false,
      ""originalTitle"": ""The Shawshank Redemption"",
      ""releaseDate"": ""1994-09-23T00:00:00"",
      ""title"": ""The Shawshank Redemption"",
      ""video"": false,
      ""backdropPath"": ""/9Xw0I5RV2ZqNLpul6lXKoviYg55.jpg"",
      ""genreIds"": [
        18,
        80
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""Framed in the 1940s for the double murder of his wife and her lover, upstanding banker Andy Dufresne begins a new life at the Shawshank prison, where he puts his accounting skills to work for an amoral warden. During his long stretch in prison, Dufresne comes to be admired by the other inmates -- including an older prisoner named Red -- for his integrity and unquenchable sense of hope."",
      ""posterPath"": ""/q6y0Go1tsGEsmtFryDOJo3dEmqu.jpg"",
      ""voteAverage"": 8.7,
      ""voteCount"": 19833,
      ""id"": 278,
      ""mediaType"": 1,
      ""popularity"": 71.638
    },
    {
      ""adult"": false,
      ""originalTitle"": ""The Godfather"",
      ""releaseDate"": ""1972-03-14T00:00:00"",
      ""title"": ""The Godfather"",
      ""video"": false,
      ""backdropPath"": ""/rSPw7tgCH9c6NqICZef4kZjFOQ5.jpg"",
      ""genreIds"": [
        18,
        80
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""Spanning the years 1945 to 1955, a chronicle of the fictional Italian-American Corleone crime family. When organized crime family patriarch, Vito Corleone barely survives an attempt on his life, his youngest son, Michael steps in to take care of the would-be killers, launching a campaign of bloody revenge."",
      ""posterPath"": ""/3bhkrj58Vtu7enYsRolD1fZdja1.jpg"",
      ""voteAverage"": 8.7,
      ""voteCount"": 14877,
      ""id"": 238,
      ""mediaType"": 1,
      ""popularity"": 72.565
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Gabriel's Inferno Part II"",
      ""releaseDate"": ""2020-07-31T00:00:00"",
      ""title"": ""Gabriel's Inferno Part II"",
      ""video"": false,
      ""backdropPath"": ""/jtAI6OJIWLWiRItNSZoWjrsUtmi.jpg"",
      ""genreIds"": [
        10749
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""Professor Gabriel Emerson finally learns the truth about Julia Mitchell's identity, but his realization comes a moment too late. Julia is done waiting for the well-respected Dante specialist to remember her and wants nothing more to do with him. Can Gabriel win back her heart before she finds love in another's arms?"",
      ""posterPath"": ""/x5o8cLZfEXMoZczTYWLrUo1P7UJ.jpg"",
      ""voteAverage"": 8.7,
      ""voteCount"": 1312,
      ""id"": 724089,
      ""mediaType"": 1,
      ""popularity"": 8.425
    },
    {
      ""adult"": false,
      ""originalTitle"": ""??????????????:||"",
      ""releaseDate"": ""2021-03-08T00:00:00"",
      ""title"": ""Evangelion: 3.0+1.0 Thrice Upon a Time"",
      ""video"": false,
      ""backdropPath"": ""/1EAxNqdkVnp48a7NUuNBHGflowM.jpg"",
      ""genreIds"": [
        16,
        28,
        18,
        878
      ],
      ""originalLanguage"": ""ja"",
      ""overview"": ""In the aftermath of the Fourth Impact, stranded without their Evangelions, Shinji, Asuka, and Rei find refuge in one of the rare pockets of humanity that still exist on the ruined planet Earth. There, each of them live a life far different from their days as an Evangelion pilot. However, the danger to the world is far from over. A new impact is looming on the horizon—one that will prove to be the true end of Evangelion."",
      ""posterPath"": ""/jDwZavHo99JtGsCyRzp4epeeBHx.jpg"",
      ""voteAverage"": 8.7,
      ""voteCount"": 333,
      ""id"": 283566,
      ""mediaType"": 1,
      ""popularity"": 211.596
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Gabriel's Inferno Part III"",
      ""releaseDate"": ""2020-11-19T00:00:00"",
      ""title"": ""Gabriel's Inferno Part III"",
      ""video"": false,
      ""backdropPath"": ""/fQq1FWp1rC89xDrRMuyFJdFUdMd.jpg"",
      ""genreIds"": [
        10749,
        35
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""The final part of the film adaption of the erotic romance novel Gabriel's Inferno written by an anonymous Canadian author under the pen name Sylvain Reynard."",
      ""posterPath"": ""/fYtHxTxlhzD4QWfEbrC1rypysSD.jpg"",
      ""voteAverage"": 8.6,
      ""voteCount"": 879,
      ""id"": 761053,
      ""mediaType"": 1,
      ""popularity"": 21.648
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Gabriel's Inferno"",
      ""releaseDate"": ""2020-05-29T00:00:00"",
      ""title"": ""Gabriel's Inferno"",
      ""video"": false,
      ""backdropPath"": ""/w2uGvCpMtvRqZg6waC1hvLyZoJa.jpg"",
      ""genreIds"": [
        10749
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""An intriguing and sinful exploration of seduction, forbidden love, and redemption, Gabriel's Inferno is a captivating and wildly passionate tale of one man's escape from his own personal hell as he tries to earn the impossible--forgiveness and love."",
      ""posterPath"": ""/oyG9TL7FcRP4EZ9Vid6uKzwdndz.jpg"",
      ""voteAverage"": 8.6,
      ""voteCount"": 2122,
      ""id"": 696374,
      ""mediaType"": 1,
      ""popularity"": 9.427
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Schindler's List"",
      ""releaseDate"": ""1993-11-30T00:00:00"",
      ""title"": ""Schindler's List"",
      ""video"": false,
      ""backdropPath"": ""/loRmRzQXZeqG78TqZuyvSlEQfZb.jpg"",
      ""genreIds"": [
        18,
        36,
        10752
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""The true story of how businessman Oskar Schindler saved over a thousand Jewish lives from the Nazis while they worked as slaves in his factory during World War II."",
      ""posterPath"": ""/sF1U4EUQS8YHUYjNl3pMGNIQyr0.jpg"",
      ""voteAverage"": 8.6,
      ""voteCount"": 11889,
      ""id"": 424,
      ""mediaType"": 1,
      ""popularity"": 36.104
    },
    {
      ""adult"": false,
      ""originalTitle"": ""?? ???"",
      ""releaseDate"": ""2020-08-22T00:00:00"",
      ""title"": ""Given"",
      ""video"": false,
      ""backdropPath"": ""/u1wHUA0R48FH4WV3sGqjwx3aNZm.jpg"",
      ""genreIds"": [
        16,
        18,
        10402,
        10749
      ],
      ""originalLanguage"": ""ja"",
      ""overview"": ""The film centers on the love relationship among the band's bassist Haruki Nakayama, drummer Akihiko Kaji, and Akihiko's roommate and ex-boyfriend Ugetsu Murata."",
      ""posterPath"": ""/xKCtoYHUyX8zAg68eemnYa2orep.jpg"",
      ""voteAverage"": 8.6,
      ""voteCount"": 255,
      ""id"": 632632,
      ""mediaType"": 1,
      ""popularity"": 134.892
    },
    {
      ""adult"": false,
      ""originalTitle"": ""The Godfather: Part II"",
      ""releaseDate"": ""1974-12-20T00:00:00"",
      ""title"": ""The Godfather: Part II"",
      ""video"": false,
      ""backdropPath"": ""/poec6RqOKY9iSiIUmfyfPfiLtvB.jpg"",
      ""genreIds"": [
        18,
        80
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""In the continuing saga of the Corleone crime family, a young Vito Corleone grows up in Sicily and in 1910s New York. In the 1950s, Michael Corleone attempts to expand the family business into Las Vegas, Hollywood and Cuba."",
      ""posterPath"": ""/hek3koDUyRQk7FIhPXsa6mT2Zc3.jpg"",
      ""voteAverage"": 8.6,
      ""voteCount"": 8932,
      ""id"": 240,
      ""mediaType"": 1,
      ""popularity"": 47.418
    },
    {
      ""adult"": false,
      ""originalTitle"": ""?????"",
      ""releaseDate"": ""2016-08-26T00:00:00"",
      ""title"": ""Your Name."",
      ""video"": false,
      ""backdropPath"": ""/dIWwZW7dJJtqC6CgWzYkNVKIUm8.jpg"",
      ""genreIds"": [
        10749,
        16,
        18
      ],
      ""originalLanguage"": ""ja"",
      ""overview"": ""High schoolers Mitsuha and Taki are complete strangers living separate lives. But one night, they suddenly switch places. Mitsuha wakes up in Taki’s body, and he in hers. This bizarre occurrence continues to happen randomly, and the two must adjust their lives around each other."",
      ""posterPath"": ""/q719jXXEzOoYaps6babgKnONONX.jpg"",
      ""voteAverage"": 8.6,
      ""voteCount"": 7936,
      ""id"": 372058,
      ""mediaType"": 1,
      ""popularity"": 142.306
    },
    {
      ""adult"": false,
      ""originalTitle"": ""????????"",
      ""releaseDate"": ""2001-07-20T00:00:00"",
      ""title"": ""Spirited Away"",
      ""video"": false,
      ""backdropPath"": ""/bf9shWfUKyEB5oB7awJeKIoCehl.jpg"",
      ""genreIds"": [
        16,
        10751,
        14
      ],
      ""originalLanguage"": ""ja"",
      ""overview"": ""A young girl, Chihiro, becomes trapped in a strange new world of spirits. When her parents undergo a mysterious transformation, she must call upon the courage she never knew she had to free her family."",
      ""posterPath"": ""/39wmItIWsg5sZMyRUHLkWBcuVCM.jpg"",
      ""voteAverage"": 8.5,
      ""voteCount"": 11900,
      ""id"": 129,
      ""mediaType"": 1,
      ""popularity"": 78.02
    },
    {
      ""adult"": false,
      ""originalTitle"": ""??"",
      ""releaseDate"": ""2013-10-02T00:00:00"",
      ""title"": ""Hope"",
      ""video"": false,
      ""backdropPath"": ""/l5K9elugftlcyIHHm4nylvsn26X.jpg"",
      ""genreIds"": [
        18
      ],
      ""originalLanguage"": ""ko"",
      ""overview"": ""After 8-year-old So-won narrowly survives a brutal sexual assault, her family labors to help her heal while coping with their own rage and grief."",
      ""posterPath"": ""/x9yjkm9gIz5qI5fJMUTfBnWiB2o.jpg"",
      ""voteAverage"": 8.5,
      ""voteCount"": 220,
      ""id"": 255709,
      ""mediaType"": 1,
      ""popularity"": 7.345
    },
    {
      ""adult"": false,
      ""originalTitle"": ""???"",
      ""releaseDate"": ""2019-05-30T00:00:00"",
      ""title"": ""Parasite"",
      ""video"": false,
      ""backdropPath"": ""/TU9NIjwzjoKPwQHoHshkFcQUCG.jpg"",
      ""genreIds"": [
        35,
        53,
        18
      ],
      ""originalLanguage"": ""ko"",
      ""overview"": ""All unemployed, Ki-taek's family takes peculiar interest in the wealthy and glamorous Parks for their livelihood until they get entangled in an unexpected incident."",
      ""posterPath"": ""/7IiTTgloJzvGI1TAYymCfbfl3vT.jpg"",
      ""voteAverage"": 8.5,
      ""voteCount"": 12251,
      ""id"": 496243,
      ""mediaType"": 1,
      ""popularity"": 112.825
    },
    {
      ""adult"": false,
      ""originalTitle"": ""The Green Mile"",
      ""releaseDate"": ""1999-12-10T00:00:00"",
      ""title"": ""The Green Mile"",
      ""video"": false,
      ""backdropPath"": ""/l6hQWH9eDksNJNiXWYRkWqikOdu.jpg"",
      ""genreIds"": [
        14,
        18,
        80
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""A supernatural tale set on death row in a Southern prison, where gentle giant John Coffey possesses the mysterious power to heal people's ailments. When the cell block's head guard, Paul Edgecomb, recognizes Coffey's miraculous gift, he tries desperately to help stave off the condemned man's execution."",
      ""posterPath"": ""/velWPhVMQeQKcxggNEU8YmIo52R.jpg"",
      ""voteAverage"": 8.5,
      ""voteCount"": 12868,
      ""id"": 497,
      ""mediaType"": 1,
      ""popularity"": 58.592
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Wolfwalkers"",
      ""releaseDate"": ""2020-10-26T00:00:00"",
      ""title"": ""Wolfwalkers"",
      ""video"": false,
      ""backdropPath"": ""/yHtB4KHNigx3ZoxDvQbW2SOXGfq.jpg"",
      ""genreIds"": [
        16,
        10751,
        12,
        14
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""In a time of superstition and magic, when wolves are seen as demonic and nature an evil to be tamed, a young apprentice hunter comes to Ireland with her father to wipe out the last pack. But when she saves a wild native girl, their friendship leads her to discover the world of the Wolfwalkers and transform her into the very thing her father is tasked to destroy."",
      ""posterPath"": ""/ehAKuE48okTuonq6TpsNQj8vFTC.jpg"",
      ""voteAverage"": 8.5,
      ""voteCount"": 574,
      ""id"": 441130,
      ""mediaType"": 1,
      ""popularity"": 22.959
    },
    {
      ""adult"": false,
      ""originalTitle"": ""12 Angry Men"",
      ""releaseDate"": ""1957-04-10T00:00:00"",
      ""title"": ""12 Angry Men"",
      ""video"": false,
      ""backdropPath"": ""/qqHQsStV6exghCM7zbObuYBiYxw.jpg"",
      ""genreIds"": [
        18
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""The defense and the prosecution have rested and the jury is filing into the jury room to decide if a young Spanish-American is guilty or innocent of murdering his father. What begins as an open and shut case soon becomes a mini-drama of each of the jurors' prejudices and preconceptions about the trial, the accused, and each other."",
      ""posterPath"": ""/e02s4wmTAExkKg0yF4dEG98ZRpK.jpg"",
      ""voteAverage"": 8.5,
      ""voteCount"": 5879,
      ""id"": 389,
      ""mediaType"": 1,
      ""popularity"": 20.534
    },
    {
      ""adult"": false,
      ""originalTitle"": ""??????????? THE MOVIE ???????????"",
      ""releaseDate"": ""2019-12-20T00:00:00"",
      ""title"": ""My Hero Academia: Heroes Rising"",
      ""video"": false,
      ""backdropPath"": ""/9guoVF7zayiiUq5ulKQpt375VIy.jpg"",
      ""genreIds"": [
        16,
        28,
        14,
        12
      ],
      ""originalLanguage"": ""ja"",
      ""overview"": ""Class 1-A visits Nabu Island where they finally get to do some real hero work. The place is so peaceful that it's more like a vacation … until they're attacked by a villain with an unfathomable Quirk! His power is eerily familiar, and it looks like Shigaraki had a hand in the plan. But with All Might retired and citizens' lives on the line, there's no time for questions. Deku and his friends are the next generation of heroes, and they're the island's only hope."",
      ""posterPath"": ""/zGVbrulkupqpbwgiNedkJPyQum4.jpg"",
      ""voteAverage"": 8.5,
      ""voteCount"": 806,
      ""id"": 592350,
      ""mediaType"": 1,
      ""popularity"": 353.867
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Black Beauty"",
      ""releaseDate"": ""2020-11-27T00:00:00"",
      ""title"": ""Black Beauty"",
      ""video"": false,
      ""backdropPath"": ""/lQAe1hfWYDdYypRVdzTbdg6JYWP.jpg"",
      ""genreIds"": [
        18
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""Born free in the American West, Black Beauty is a horse rounded up and brought to Birtwick Stables, where she meets spirited teenager Jo Green. The two forge a bond that carries Beauty through the different chapters, challenges and adventures."",
      ""posterPath"": ""/5ZjMNJJabwBEnGVQoR2yoMEL9um.jpg"",
      ""voteAverage"": 8.5,
      ""voteCount"": 221,
      ""id"": 526702,
      ""mediaType"": 1,
      ""popularity"": 14.516
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Pulp Fiction"",
      ""releaseDate"": ""1994-09-10T00:00:00"",
      ""title"": ""Pulp Fiction"",
      ""video"": false,
      ""backdropPath"": ""/suaEOtk1N1sgg2MTM7oZd2cfVp3.jpg"",
      ""genreIds"": [
        53,
        80
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""A burger-loving hit man, his philosophical partner, a drug-addled gangster's moll and a washed-up boxer converge in this sprawling, comedic crime caper. Their adventures unfurl in three stories that ingeniously trip back and forth in time."",
      ""posterPath"": ""/d5iIlFn5s0ImszYzBPb8JPIfbXD.jpg"",
      ""voteAverage"": 8.5,
      ""voteCount"": 21875,
      ""id"": 680,
      ""mediaType"": 1,
      ""popularity"": 55.213
    }
  ],
  ""totalPages"": 460,
  ""totalResults"": 9200
}";

        private const string _upcomingJson = @"{
  ""dates"": {
    ""maximum"": ""2021-10-31T00:00:00"",
    ""minimum"": ""2021-10-16T00:00:00""
  },
  ""page"": 1,
  ""results"": [
    {
      ""adult"": false,
      ""originalTitle"": ""Venom: Let There Be Carnage"",
      ""releaseDate"": ""2021-09-30T00:00:00"",
      ""title"": ""Venom: Let There Be Carnage"",
      ""video"": false,
      ""backdropPath"": ""/t9nyF3r0WAlJ7Kr6xcRYI4jr9jm.jpg"",
      ""genreIds"": [
        878,
        28
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""After finding a host body in investigative reporter Eddie Brock, the alien symbiote must face a new enemy, Carnage, the alter ego of serial killer Cletus Kasady."",
      ""posterPath"": ""/rjkmN1dniUHVYAtwuV3Tji7FsDO.jpg"",
      ""voteAverage"": 7.5,
      ""voteCount"": 346,
      ""id"": 580489,
      ""mediaType"": 1,
      ""popularity"": 11173.275
    },
    {
      ""adult"": false,
      ""originalTitle"": ""The Addams Family 2"",
      ""releaseDate"": ""2021-10-01T00:00:00"",
      ""title"": ""The Addams Family 2"",
      ""video"": false,
      ""backdropPath"": ""/kTOheVmqSBDIRGrQLv2SiSc89os.jpg"",
      ""genreIds"": [
        16,
        35,
        10751
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""The Addams get tangled up in more wacky adventures and find themselves involved in hilarious run-ins with all sorts of unsuspecting characters."",
      ""posterPath"": ""/xYLBgw7dHyEqmcrSk2Sq3asuSq5.jpg"",
      ""voteAverage"": 7.8,
      ""voteCount"": 189,
      ""id"": 639721,
      ""mediaType"": 1,
      ""popularity"": 2112.639
    },
    {
      ""adult"": false,
      ""originalTitle"": ""The Boss Baby: Family Business"",
      ""releaseDate"": ""2021-07-01T00:00:00"",
      ""title"": ""The Boss Baby: Family Business"",
      ""video"": false,
      ""backdropPath"": ""/akwg1s7hV5ljeSYFfkw7hTHjVqk.jpg"",
      ""genreIds"": [
        16,
        35,
        12,
        10751
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""The Templeton brothers — Tim and his Boss Baby little bro Ted — have become adults and drifted away from each other. But a new boss baby with a cutting-edge approach and a can-do attitude is about to bring them together again … and inspire a new family business."",
      ""posterPath"": ""/uWStkK8bq9ixY3fc7y209ZleCoF.jpg"",
      ""voteAverage"": 7.7,
      ""voteCount"": 1481,
      ""id"": 459151,
      ""mediaType"": 1,
      ""popularity"": 1083.327
    },
    {
      ""adult"": false,
      ""originalTitle"": ""After We Fell"",
      ""releaseDate"": ""2021-09-01T00:00:00"",
      ""title"": ""After We Fell"",
      ""video"": false,
      ""backdropPath"": ""/qD45xHA35HdJDGOaA1AgDwiWEgO.jpg"",
      ""genreIds"": [
        10749,
        18
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""Just as Tessa's life begins to become unglued, nothing is what she thought it would be. Not her friends nor her family. The only person that she should be able to rely on is Hardin, who is furious when he discovers the massive secret that she's been keeping. Before Tessa makes the biggest decision of her life, everything changes because of revelations about her family."",
      ""posterPath"": ""/3WfvjNWr5k1Zzww81b3GWc8KQhb.jpg"",
      ""voteAverage"": 8.1,
      ""voteCount"": 325,
      ""id"": 744275,
      ""mediaType"": 1,
      ""popularity"": 772.466
    },
    {
      ""adult"": false,
      ""originalTitle"": ""No Time to Die"",
      ""releaseDate"": ""2021-09-29T00:00:00"",
      ""title"": ""No Time to Die"",
      ""video"": false,
      ""backdropPath"": ""/u5Fp9GBy9W8fqkuGfLBuuoJf57Z.jpg"",
      ""genreIds"": [
        12,
        28,
        53
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""Bond has left active service and is enjoying a tranquil life in Jamaica. His peace is short-lived when his old friend Felix Leiter from the CIA turns up asking for help. The mission to rescue a kidnapped scientist turns out to be far more treacherous than expected, leading Bond onto the trail of a mysterious villain armed with dangerous new technology."",
      ""posterPath"": ""/iUgygt3fscRoKWCV1d0C7FbM9TP.jpg"",
      ""voteAverage"": 7.4,
      ""voteCount"": 460,
      ""id"": 370172,
      ""mediaType"": 1,
      ""popularity"": 779.673
    },
    {
      ""adult"": false,
      ""originalTitle"": ""The Courier"",
      ""releaseDate"": ""2021-03-18T00:00:00"",
      ""title"": ""The Courier"",
      ""video"": false,
      ""backdropPath"": ""/3pIqd1hgZ2xqzWEyiYp4blqE9Fi.jpg"",
      ""genreIds"": [
        53,
        36,
        18
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""Cold War spy Greville Wynne and his Russian source try to put an end to the Cuban Missile Crisis."",
      ""posterPath"": ""/zFIjKtZrzhmc7HecdFXXjsLR2Ig.jpg"",
      ""voteAverage"": 7.1,
      ""voteCount"": 413,
      ""id"": 522241,
      ""mediaType"": 1,
      ""popularity"": 646.757
    },
    {
      ""adult"": false,
      ""originalTitle"": ""The Many Saints of Newark"",
      ""releaseDate"": ""2021-09-22T00:00:00"",
      ""title"": ""The Many Saints of Newark"",
      ""video"": false,
      ""backdropPath"": ""/hrzoy8vvUrxQixOM11pwW9AX7Bu.jpg"",
      ""genreIds"": [
        80,
        18
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""Young Anthony Soprano is growing up in one of the most tumultuous eras in Newark, N.J., history, becoming a man just as rival gangsters start to rise up and challenge the all-powerful DiMeo crime family. Caught up in the changing times is the uncle he idolizes, Dickie Moltisanti, whose influence over his nephew will help shape the impressionable teenager into the all-powerful mob boss, Tony Soprano."",
      ""posterPath"": ""/1UkbPQspPbq1FPbFP4VV1ELCfSN.jpg"",
      ""voteAverage"": 6.8,
      ""voteCount"": 88,
      ""id"": 524369,
      ""mediaType"": 1,
      ""popularity"": 693.094
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Dune"",
      ""releaseDate"": ""2021-09-15T00:00:00"",
      ""title"": ""Dune"",
      ""video"": false,
      ""backdropPath"": ""/aknvFyJUQQoZFtmFnYzKi4vGv4J.jpg"",
      ""genreIds"": [
        28,
        12,
        18,
        878
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""Paul Atreides, a brilliant and gifted young man born into a great destiny beyond his understanding, must travel to the most dangerous planet in the universe to ensure the future of his family and his people. As malevolent forces explode into conflict over the planet's exclusive supply of the most precious resource in existence-a commodity capable of unlocking humanity's greatest potential-only those who can conquer their fear will survive."",
      ""posterPath"": ""/s1FhMAr91WL8D5DeHOcuBELtiHJ.jpg"",
      ""voteAverage"": 8.1,
      ""voteCount"": 1408,
      ""id"": 438631,
      ""mediaType"": 1,
      ""popularity"": 416.541
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Black Water: Abyss"",
      ""releaseDate"": ""2020-07-09T00:00:00"",
      ""title"": ""Black Water: Abyss"",
      ""video"": false,
      ""backdropPath"": ""/fRrpOILyXuWaWLmqF7kXeMVwITQ.jpg"",
      ""genreIds"": [
        27,
        53,
        12,
        9648
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""An adventure-loving couple convince their friends to explore a remote, uncharted cave system in the forests of Northern Australia. With a tropical storm approaching, they abseil into the mouth of the cave, but when the caves start to flood, tensions rise as oxygen levels fall and the friends find themselves trapped. Unknown to them, the storm has also brought in a pack of dangerous and hungry crocodiles."",
      ""posterPath"": ""/95S6PinQIvVe4uJAd82a2iGZ0rA.jpg"",
      ""voteAverage"": 5.1,
      ""voteCount"": 274,
      ""id"": 522444,
      ""mediaType"": 1,
      ""popularity"": 404.685
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Halloween Kills"",
      ""releaseDate"": ""2021-10-14T00:00:00"",
      ""title"": ""Halloween Kills"",
      ""video"": false,
      ""backdropPath"": ""/kIG81E0H4CxuYIRgyNyD0z3KSlM.jpg"",
      ""genreIds"": [
        27,
        53,
        80
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""After Laurie, Karen and Allyson leave the masked monster Michael Myers caged and burning in Laurie's basement, Laurie is rushed to the hospital with fatal injuries, believing she has finally killed her algorithm.  But when Michael manages to free himself from Laurie's trap, his bloodbath ritual begins again. As she fights her pain and prepares to defend against it, she inspires everyone in Haddonfield to rise up against The Shape."",
      ""posterPath"": ""/snHgrt2SAEHdI8LLUzV1HgHqPlr.jpg"",
      ""voteAverage"": 5,
      ""voteCount"": 1,
      ""id"": 610253,
      ""mediaType"": 1,
      ""popularity"": 208.885
    },
    {
      ""adult"": false,
      ""originalTitle"": ""??????????? THE MOVIE ???? ????? ?????"",
      ""releaseDate"": ""2021-08-06T00:00:00"",
      ""title"": ""My Hero Academia: World Heroes' Mission"",
      ""video"": false,
      ""backdropPath"": ""/2RHjd10wqv57xYzZkNK8Sl09Ddt.jpg"",
      ""genreIds"": [
        16,
        28,
        35,
        14,
        12
      ],
      ""originalLanguage"": ""ja"",
      ""overview"": ""A mysterious group called Humarize strongly believes in the Quirk Singularity Doomsday theory which states that when quirks get mixed further in with future generations, that power will bring forth the end of humanity. In order to save everyone, the Pro-Heroes around the world ask UA Academy heroes-in-training to assist them and form a world-classic selected hero team. It is up to the heroes to save the world and the future of heroes in what is the most dangerous crisis to take place yet in My Hero Academy."",
      ""posterPath"": ""/4NUzcKtYPKkfTwKsLjwNt8nRIXV.jpg"",
      ""voteAverage"": 6.9,
      ""voteCount"": 11,
      ""id"": 768744,
      ""mediaType"": 1,
      ""popularity"": 159.567
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Cry Macho"",
      ""releaseDate"": ""2021-09-16T00:00:00"",
      ""title"": ""Cry Macho"",
      ""video"": false,
      ""backdropPath"": ""/g6wufgtycJCP508tlC3crSYFCgC.jpg"",
      ""genreIds"": [
        37,
        18
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""Mike Milo, a one-time rodeo star and washed-up horse breeder, takes a job from an ex-boss to bring the man's young son home from Mexico."",
      ""posterPath"": ""/wqaaWAlXgLNGdAemU7wNvFZ70hr.jpg"",
      ""voteAverage"": 6.8,
      ""voteCount"": 164,
      ""id"": 749274,
      ""mediaType"": 1,
      ""popularity"": 133.899
    },
    {
      ""adult"": false,
      ""originalTitle"": ""The Ice Road"",
      ""releaseDate"": ""2021-06-24T00:00:00"",
      ""title"": ""The Ice Road"",
      ""video"": false,
      ""backdropPath"": ""/5MlvT4DZIdkpb7A9t375HVoiJ1v.jpg"",
      ""genreIds"": [
        28,
        12,
        18,
        53
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""After a remote diamond mine collapses in far northern Canada, an ice road driver must lead an impossible rescue mission over a frozen ocean to save the trapped miners."",
      ""posterPath"": ""/eOH9yh5r7QD4n94gbp7kULf2ft6.jpg"",
      ""voteAverage"": 7.2,
      ""voteCount"": 682,
      ""id"": 646207,
      ""mediaType"": 1,
      ""popularity"": 140.959
    },
    {
      ""adult"": false,
      ""originalTitle"": ""CODA"",
      ""releaseDate"": ""2021-07-30T00:00:00"",
      ""title"": ""CODA"",
      ""video"": false,
      ""backdropPath"": ""/4Tz8V8aRim8cFgKEWprSUjBy8tY.jpg"",
      ""genreIds"": [
        18,
        10402,
        10749
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""As a CODA (Child of Deaf Adults), Ruby is the only hearing person in her deaf family. When the family's fishing business is threatened, Ruby finds herself torn between pursuing her love of music and her fear of abandoning her parents."",
      ""posterPath"": ""/BzVjmm8l23rPsijLiNLUzuQtyd.jpg"",
      ""voteAverage"": 8.3,
      ""voteCount"": 218,
      ""id"": 776503,
      ""mediaType"": 1,
      ""popularity"": 113.326
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Ron's Gone Wrong"",
      ""releaseDate"": ""2021-10-15T00:00:00"",
      ""title"": ""Ron's Gone Wrong"",
      ""video"": false,
      ""backdropPath"": ""/99OiyEOsFecBiS5XkhQAB98rlP6.jpg"",
      ""genreIds"": [
        16,
        878,
        10751,
        35
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""In a world where walking, talking, digitally connected bots have become children's best friends, an 11-year-old finds that his robot buddy doesn't quite work the same as the others do."",
      ""posterPath"": ""/gA9QxSravC2EVEkEKgyEmDrfL0e.jpg"",
      ""voteAverage"": 0,
      ""voteCount"": 0,
      ""id"": 482321,
      ""mediaType"": 1,
      ""popularity"": 101.439
    },
    {
      ""adult"": false,
      ""originalTitle"": ""The Last Duel"",
      ""releaseDate"": ""2021-10-13T00:00:00"",
      ""title"": ""The Last Duel"",
      ""video"": false,
      ""backdropPath"": ""/4BOgSGwEEHZuDhiHUOsrXaJBqoc.jpg"",
      ""genreIds"": [
        18,
        36
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""King Charles VI declares that Knight Jean de Carrouges settle his dispute with his squire by challenging him to a duel."",
      ""posterPath"": ""/lDSLp0tjy5fYwYTRoLZzUxJ65w0.jpg"",
      ""voteAverage"": 7.3,
      ""voteCount"": 3,
      ""id"": 617653,
      ""mediaType"": 1,
      ""popularity"": 83.535
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Last Night in Soho"",
      ""releaseDate"": ""2021-10-09T00:00:00"",
      ""title"": ""Last Night in Soho"",
      ""video"": false,
      ""backdropPath"": ""/7OcRErUXXdAVAHg6y5cjn56ivtu.jpg"",
      ""genreIds"": [
        27,
        18,
        53,
        9648
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""A young girl, passionate about fashion design, is mysteriously able to enter the 1960s where she encounters her idol, a dazzling wannabe singer. But 1960s London is not what it seems, and time seems to be falling apart with shady consequences."",
      ""posterPath"": ""/1qamhFbRGHicXz7hAzcsOnKVyFL.jpg"",
      ""voteAverage"": 9,
      ""voteCount"": 1,
      ""id"": 576845,
      ""mediaType"": 1,
      ""popularity"": 49.298
    },
    {
      ""adult"": false,
      ""originalTitle"": ""The Card Counter"",
      ""releaseDate"": ""2021-09-03T00:00:00"",
      ""title"": ""The Card Counter"",
      ""video"": false,
      ""backdropPath"": ""/r7dJqfmspzVlHYWXBiKn7NP8ZBw.jpg"",
      ""genreIds"": [
        18,
        53,
        80
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""William Tell just wants to play cards. His spartan existence on the casino trail is shattered when he is approached by Cirk, a vulnerable and angry young man seeking help to execute his plan for revenge on a military colonel. Tell sees a chance at redemption through his relationship with Cirk. But keeping Cirk on the straight-and-narrow proves impossible, dragging Tell back into the darkness of his past."",
      ""posterPath"": ""/60AQOrByV9vDCqy6nt4eOroRild.jpg"",
      ""voteAverage"": 6.6,
      ""voteCount"": 134,
      ""id"": 643532,
      ""mediaType"": 1,
      ""popularity"": 54.481
    },
    {
      ""adult"": false,
      ""originalTitle"": ""The Rescue"",
      ""releaseDate"": ""2021-10-08T00:00:00"",
      ""title"": ""The Rescue"",
      ""video"": false,
      ""backdropPath"": ""/nyrKuuvydxq04JN4OsdhVNLiWi2.jpg"",
      ""genreIds"": [
        99,
        53
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""Based on the true event which happened in 2018 in Thailand."",
      ""posterPath"": ""/gsUFkojEseXhCQHdLWkgwHXUYdJ.jpg"",
      ""voteAverage"": 0,
      ""voteCount"": 0,
      ""id"": 680058,
      ""mediaType"": 1,
      ""popularity"": 34.444
    },
    {
      ""adult"": false,
      ""originalTitle"": ""Penguin Bloom"",
      ""releaseDate"": ""2021-01-21T00:00:00"",
      ""title"": ""Penguin Bloom"",
      ""video"": false,
      ""backdropPath"": ""/2dWWf6qjVU0TklCUKCUra1Yqfz3.jpg"",
      ""genreIds"": [
        18
      ],
      ""originalLanguage"": ""en"",
      ""overview"": ""When an unlikely ally enters the Bloom family's world in the form of an injured baby magpie they name Penguin, the bird’s arrival makes a profound difference in the struggling family’s life."",
      ""posterPath"": ""/iUn7594Rwfstu5njA8hf9WQIcFi.jpg"",
      ""voteAverage"": 7.1,
      ""voteCount"": 137,
      ""id"": 618416,
      ""mediaType"": 1,
      ""popularity"": 57.94
    }
  ],
  ""totalPages"": 13,
  ""totalResults"": 246
}";
    }
}
