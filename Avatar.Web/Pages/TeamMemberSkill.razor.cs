using Avatar.Web.Components;
using Avatar.Web.Models;
using Blazorise;

namespace Avatar.Web.Pages
{
    public partial class TeamMemberSkill
    {
        private Modal createModal;
        private Modal editModal;
        private AlertModal alertModal;
        private ConfirmDialog confirmDialogRef;
        private Validations validations;
        public List<TeamMemberSkillModel> TeamMemberSkills = new List<TeamMemberSkillModel>();
        public List<SkillModel> Skills = new List<SkillModel>();
        public List<SkillModel> FilteredSkills = new List<SkillModel>();
        public List<TeamMemberModel> TeamMembers = new List<TeamMemberModel>();
        private TeamMemberModel teamMemberToEdit = new();
        private TeamMemberSkillModel selectedAssignment = new();
        private TeamMemberModel selectedTeamMember = new TeamMemberModel();
        private TeamMemberSkillModel assignmentToDelete = new();
        public string Message { get; set; } = string.Empty;
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            // Load team member skills
            var assignmentsResult = await teamMemberSkillApiHandler.GetTeamMemberSkillList();
            if (assignmentsResult.Succeeded && assignmentsResult.Result != null && assignmentsResult.Result.IsSuccess)
            {
                TeamMemberSkills = assignmentsResult.Result.Result.Select(a => new TeamMemberSkillModel
                {
                    Id = a.Id,
                    TeamMemberId = a.TeamMemberId,
                    SkillId = a.SkillId,
                    Level = a.Level,
                }).ToList();
            }

            // Load skills for dropdown
            var skillsResult = await skillApiHandler.GetSkillList();
            if (skillsResult.Succeeded && skillsResult.Result != null && skillsResult.Result.IsSuccess)
            {
                Skills = skillsResult.Result.Result.Select(s => new SkillModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description
                }).ToList();
            }

            // Load team members for dropdown
            var membersResult = await teamMemberApiHandler.GetTeamMemberList();
            if (membersResult.Succeeded && membersResult.Result != null && membersResult.Result.IsSuccess)
            {
                TeamMembers = membersResult.Result.Result.Select(m => new TeamMemberModel
                {
                    Id = m.Id,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    Title = m.Title,
                }).ToList();
            }

        }
        private async Task ShowModal(TeamMemberSkillModel assignment)
        {
            selectedAssignment = assignment != null ?
                new TeamMemberSkillModel
                {
                    Id = assignment.Id,
                    TeamMemberId = selectedTeamMember.Id,
                    SkillId = assignment.SkillId,
                    Level = assignment.Level
                } :
                new TeamMemberSkillModel();

            await editModal.Show();
        }

        private async Task HideModal()
        {
            await editModal.Hide();
        }
        private async Task SaveAssignment()
        {
            if (await validations.ValidateAll())
            {
                var create = await teamMemberSkillApiHandler.CreateTeamMemberSkill(
                    new Framework.ApiCommand.TeamMemberSkill.Request.CreateTeamMemberSkillArgs
                    {
                        TeamMemberId = selectedAssignment.TeamMemberId,
                        SkillId = selectedAssignment.SkillId,
                        Level = selectedAssignment.Level
                    });

                if (create.Succeeded && create.Result.IsSuccess)
                {
                    Message = "Skill assignment created successfully!";
                    await alertModal.Show();
                    // Refresh skills for the selected member
                    await LoadSelectedTeamMemberSkills();
                    await HideCreateModal();
                }
                else
                {
                    Message = "Failed to create skill assignment.";
                    await alertModal.Show();
                }
            }
        }
        private async Task UpdateAssignment()
        {
            var update = await teamMemberSkillApiHandler.UpdateTeamMemberSkill(
                new Framework.ApiCommand.TeamMemberSkill.Request.UpdateTeamMemberSkillArgs
                {
                    Id = selectedAssignment.Id,
                    TeamMemberId = selectedAssignment.TeamMemberId,
                    SkillId = selectedAssignment.SkillId,
                    Level = selectedAssignment.Level
                });

            if (update.Succeeded && update.Result.IsSuccess)
            {
                Message = "Skill assignment updated successfully!";
                await alertModal.Show();
                // Refresh skills for the selected member
                await LoadSelectedTeamMemberSkills();
                await HideEditModal();
            }
            else
            {
                Message = "Failed to update assignment.";
                await alertModal.Show();
            }
        }
        private async Task DeleteAssignment(TeamMemberSkillModel assignment)
        {
            assignmentToDelete = assignment;
            await confirmDialogRef.Open();
        }

        private async Task ConfirmDeleteChanged(bool value)
        {
            if (value)
            {
                var deleteResult = await teamMemberSkillApiHandler.DeleteTeamMemberSkill(
                    new Framework.ApiCommand.TeamMemberSkill.Request.DeleteTeamMemberSkillArgs
                    {
                        Id = assignmentToDelete.Id
                    });

                if (deleteResult.Succeeded && deleteResult.Result.IsSuccess)
                {
                    Message = "Assignment removed successfully!";
                    await alertModal.Show();
                    await LoadSelectedTeamMemberSkills();
                    StateHasChanged();
                }
                else
                {
                    Message = "Failed to remove assignment.";
                    await alertModal.Show();
                }
            }
        }
        private async Task SelectTeamMember(TeamMemberModel member)
        {
            selectedTeamMember = member;
            // Filter skills for the selected member
            await LoadSelectedTeamMemberSkills();
        }
        public string GetTeamMemberName(int teamMemberId)
        {
            var member = TeamMembers.FirstOrDefault(a => a.Id == teamMemberId);
            return member != null ? $"{member.FirstName} {member.LastName}" : "Unknown";
        }
        private string GetSkillName(int skillId)
        {
            return Skills.FirstOrDefault(a => a.Id == skillId)?.Name ?? "Unknown";
        }
        private async Task ShowCreateModal()
        {
            selectedAssignment = new TeamMemberSkillModel
            {
                TeamMemberId = selectedTeamMember.Id,
            };
            FilterSkills();
            await createModal.Show();
        }

        private async Task ShowEditModal(TeamMemberSkillModel assignment)
        {
            selectedAssignment = new TeamMemberSkillModel
            {
                Id = assignment.Id,
                TeamMemberId = assignment.TeamMemberId,
                SkillId = assignment.SkillId,
                Level = assignment.Level
            };
            FilterSkills();
            await editModal.Show();
        }
        private async Task HideCreateModal() => await createModal.Hide();
        private async Task HideEditModal() => await editModal.Hide();
        private void FilterSkills()
        {
            // Filter skills to only include those not already assigned to the selected team member
            var assignedSkillIds = TeamMemberSkills
                .Where(s => s.TeamMemberId == selectedTeamMember.Id)
                .Select(s => s.SkillId)
                .ToList();

            FilteredSkills = Skills.Where(skill => !assignedSkillIds.Contains(skill.Id)).ToList();

        }
        private async Task LoadSelectedTeamMemberSkills()
        {
            await LoadData();
            // Filter skills for the selected member
            TeamMemberSkills = TeamMemberSkills
                .Where(s => s.TeamMemberId == selectedTeamMember.Id)
                .ToList();
        }
    }
}