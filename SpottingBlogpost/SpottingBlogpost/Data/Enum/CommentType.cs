using System.ComponentModel;

namespace SpottingBlogpost.Data.Enum
{
    public enum CommentType
    {
        [Description("There are some flaws in the spotting info that I'd like to address")]
        Critic,
        [Description("I'd like to add more info/context about this particular ship")]
        Context,
        [Description("I have an opinion on this ship that I'd like to share")]
        Opinion,
        [Description("I have some experiences/encounters/stories with this particular ship, that I'd like to share")]
        Other
    }
}
