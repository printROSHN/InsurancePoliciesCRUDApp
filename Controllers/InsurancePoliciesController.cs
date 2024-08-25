using InsurancePoliciesCRUDApp.Data;   // Ensure this matches your actual namespace
using InsurancePoliciesCRUDApp.Models; // Ensure this matches your actual namespace
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InsurancePoliciesCRUDApp.Controllers  // Ensure this matches your actual namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancePoliciesController : ControllerBase
    {
        private readonly InsurancePolicyRepository _policyRepository;

        public InsurancePoliciesController(InsurancePolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        // GET: api/insurancepolicies
        [HttpGet]
        public IEnumerable<InsurancePolicy> GetPolicies()
        {
            return _policyRepository.GetAllPolicies();
        }

        // GET: api/insurancepolicies/5
        [HttpGet("{id}")]
        public ActionResult<InsurancePolicy> GetPolicy(int id)
        {
            var policy = _policyRepository.GetPolicyById(id);

            if (policy == null)
            {
                return NotFound();
            }

            return policy;
        }

        // POST: api/insurancepolicies
        [HttpPost]
        public IActionResult PostPolicy(InsurancePolicy policy)
        {
            _policyRepository.AddPolicy(policy);
            return CreatedAtAction(nameof(GetPolicy), new { id = policy.Id }, policy);
        }

        // PUT: api/insurancepolicies/5
        [HttpPut("{id}")]
        public IActionResult PutPolicy(int id, InsurancePolicy policy)
        {
            if (id != policy.Id)
            {
                return BadRequest();
            }

            _policyRepository.UpdatePolicy(policy);
            return NoContent();
        }

        // DELETE: api/insurancepolicies/5
        [HttpDelete("{id}")]
        public IActionResult DeletePolicy(int id)
        {
            var policy = _policyRepository.GetPolicyById(id);
            if (policy == null)
            {
                return NotFound();
            }

            _policyRepository.DeletePolicy(id);
            return NoContent();
        }
    }
}
