namespace GM.Models;

public class User
{
    public string Username { get; }

    //private readonly IUserRepository _userRepository;
    //private readonly IDocumentRepository _documentRepo;
    //private readonly IDocumentInfoRepository _docInfoRepo;

    public User(string username) => Username = username;


    //public async Task<User> GetUser()
    //{
    //    Entity.User user = await _userRepository.GetUser(Username);

    //    return new User(user.Username);
    //}

    //public async Task<IEnumerable<Document>> GetAllDocuments()
    //{
    //    return await _documentRepo.GetAllDocumentsAsync();
    //}

    //public async Task AddDocumentName(Document document)
    //{
    //    //foreach (Document existingDoc in _documents)
    //    //{
    //    //    if (existingDoc.ConflictDocumentName(document))
    //    //    {
    //    //        throw new DocumentException();
    //    //    }
    //    //}

    //    await _documentRepo.InsertDocumentNameAsync(document);
    //}

    //public async Task<IEnumerable<DocumentInfoModel>> GetAllDocumentInfos()
    //{
    //    return await _docInfoRepo.GetDocumentInfosAsync();
    //}

    //internal void AddDocumentDetail(DocumentInfoModel docDetail)
    //{
    //    _docDetails.Add(docDetail);
    //}

    //public IEnumerable<Vehicle> GetAllVehicles()
    //{
    //    return _vehicles;
    //}

    //public void AddVehicle(Vehicle vehicle)
    //{
    //    foreach (Vehicle existingVehicle in _vehicles)
    //    {
    //        if (existingVehicle.ConflictVehicleCode(vehicle))
    //        {
    //            throw new VehicleException();
    //        }
    //    }

    //    _vehicles.Add(vehicle);
    //}
}
