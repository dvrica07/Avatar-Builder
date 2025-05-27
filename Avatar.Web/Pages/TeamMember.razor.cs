using Avatar.Web.Components;
using Avatar.Web.Models;
using Blazorise;

namespace Avatar.Web.Pages
{
    public partial class TeamMember
    {
        private Modal editModal;
        private Modal viewModal;
        private AlertModal alertModal;
        private ConfirmDialog confirmDialogRef;
        private Validations validations;
        public List<TeamMemberModel> TeamMembers = new List<TeamMemberModel>();
        private TeamMemberModel selectedMember = new();
        private TeamMemberModel memberToDelete = new();
        public string Message { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadTeamMembers();
        }

        private async Task LoadTeamMembers()
        {
            var result = await teamMemberApiHandler.GetTeamMemberList();
            if (result.Succeeded && result.Result != null && result.Result.IsSuccess)
            {
                TeamMembers = result.Result.Result.Select(m => new TeamMemberModel
                {
                    Id = m.Id,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    Title = m.Title
                }).ToList();
            }
            else
            {
                Message = "Failed to load team members.";
                await alertModal.Show();
            }
        }

        private async Task ShowModal(TeamMemberModel member)
        {
            selectedMember = member ?? new TeamMemberModel(); // Reset if null
            await editModal.Show();
        }

        private async Task HideModal()
        {
            await editModal.Hide();
        }

        private async Task ViewMember(TeamMemberModel member)
        {
            selectedMember = member;
            await viewModal.Show();
        }

        private async Task HideViewModal()
        {
            await viewModal.Hide();
        }

        private async Task SaveMember()
        {
            if (await validations.ValidateAll())
            {
                // Check for existing member (excluding current if updating)
                bool memberExists = TeamMembers.Any(m =>
                    m.FirstName.Trim().ToLower() == selectedMember.FirstName.Trim().ToLower() &&
                    m.LastName.Trim().ToLower() == selectedMember.LastName.Trim().ToLower());

                if (memberExists)
                {
                    Message = "Member already exists.";
                    await alertModal.Show();
                    return;
                }

                if (selectedMember.Id == 0)
                {
                    // Add new team member
                    var createResult = await teamMemberApiHandler.CreateTeamMember(
                        new Framework.ApiCommand.TeamMember.Request.CreateTeamMemberArgs
                        {
                            FirstName = selectedMember.FirstName,
                            LastName = selectedMember.LastName,
                            Title = selectedMember.Title,
                        });

                    if (createResult.Succeeded && createResult.Result.IsSuccess)
                    {
                        Message = "Team member created successfully!";
                        await alertModal.Show();
                    }
                    else
                    {
                        Message = "Failed to create team member.";
                        await alertModal.Show();
                    }
                }
                else
                {
                    // Update existing team member
                    var updateResult = await teamMemberApiHandler.UpdateTeamMember(
                        new Framework.ApiCommand.TeamMember.Request.UpdateTeamMembeArgs
                        {
                            Id = selectedMember.Id,
                            FirstName = selectedMember.FirstName,
                            LastName = selectedMember.LastName,
                            Title = selectedMember.Title
                        });

                    if (updateResult.Succeeded && updateResult.Result.IsSuccess)
                    {
                        Message = "Team member updated successfully!";
                        await alertModal.Show();
                    }
                    else
                    {
                        Message = "Failed to update team member.";
                        await alertModal.Show();
                    }
                }

                await HideModal();
                await LoadTeamMembers();
                StateHasChanged();
            }
        }

        private async Task DeleteMember(TeamMemberModel member)
        {
            memberToDelete = member;
            await confirmDialogRef.Open();
        }

        private async Task ConfirmDeleteChanged(bool value)
        {
            if (value)
            {
                // First check if member has any skills assigned
                var hasSkills = await CheckMemberHasSkills(memberToDelete.Id);
                if (hasSkills)
                {
                    Message = "Cannot delete team member with assigned skills. Remove skills first.";
                    await alertModal.Show();
                    return;
                }

                var deleteResult = await teamMemberApiHandler.DeleteTeamMember(
                    new Framework.ApiCommand.TeamMember.Request.DeleteTeamMemberArgs
                    {
                        Id = memberToDelete.Id
                    });

                if (deleteResult.Succeeded && deleteResult.Result.IsSuccess)
                {
                    Message = "Team member deleted successfully!";
                    await alertModal.Show();
                    await LoadTeamMembers(); // Refresh the data
                    StateHasChanged();
                }
                else
                {
                    Message = "Failed to delete team member.";
                    await alertModal.Show();
                }
            }
        }

        private async Task<bool> CheckMemberHasSkills(int memberId)
        {
            // You would need to implement this check with your API
            // This is just a placeholder implementation
            var skillsResult = await teamMemberSkillApiHandler.GetTeamMemberSkillList();
            if (skillsResult.Succeeded && skillsResult.Result != null && skillsResult.Result.IsSuccess)
            {
                return skillsResult.Result.Result.Any(s => s.TeamMemberId == memberId);
            }
            return false;
        }
    }
}