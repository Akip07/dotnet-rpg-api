
namespace dotnet_rpg.Services.CharacterService
{
    
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character{Id = 1, Name = "Sans"},
        };

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            var character = _mapper.Map<Character>(newCharacter);

            //character.Id = dbCharacters.Max(c => c.Id) + 1;
            _context.Characters.Attach(character);
            _context.SaveChanges();
            dbCharacters = await _context.Characters.ToListAsync();

            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            try {
            var character = dbCharacters.FirstOrDefault(c => c.Id == id);
            if(character == null)
                throw new Exception($"Character with ID '{id}' not found");
            
            _context.Characters.Remove(character);
            _context.SaveChanges();
            dbCharacters = await _context.Characters.ToListAsync();
        
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch(Exception ex) {
                serviceResponse.Success =false;
                serviceResponse.Message = ex.Message;

            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacters = await _context.Characters.ToListAsync();
            var character = dbCharacters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceResponse;

        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacters = await _context.Characters.ToListAsync();
                try {
                var character = _context.Characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
                if(character == null)
                    throw new Exception($"Character with ID '{updatedCharacter.Id}' not found");

                _mapper.Map(updatedCharacter, character);
                // character.Name = updatedCharacter.Name;
                // character.Strength = updatedCharacter.Strength;
                // character.Defense = updatedCharacter.Defense;
                // character.Class = updatedCharacter.Class;
                // character.HitPoints = updatedCharacter.HitPoints;
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                _context.SaveChanges();
                }
                catch(Exception ex) {
                    serviceResponse.Success =false;
                    serviceResponse.Message = ex.Message;

                }
                return serviceResponse;

        }
    }
}