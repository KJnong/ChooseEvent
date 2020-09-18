using ChooseEvent2.Models;
using System.Collections.Generic;

namespace ChooseEvent2.Repositories
{
    public interface IRelationshipRepository
    {
        List<Relationship> GetRelationships();
        void RemoveRelationship(Relationship relationship);
        void AddRelationship(Relationship relationship);

    }
    
}