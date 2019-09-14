import { User } from '@/user';
import { Vote, VoteResourceType, VotableEntity } from '@/vote';

/**
 * A text comment attached to a post.
 */
export class Comment extends VotableEntity {
    /**
     * The maximum number of characters allowed in the comment body.
     */
    public static BODY_MAX_LENGTH = 10_000;

    /**
     * The number of comments per page.
     */
    public static PAGE_SIZE = 10;

    /**
     * The children on the comment.
     */
    public children: Comment[];

    /**
     * The type of vote resource it is.
     */
    public voteResourceType: VoteResourceType = VoteResourceType.Comment;

    /**
     *
     * @param id The ID of the comment.
     * @param user The user who posted it.
     * @param body The body (text) of the comment.
     * @param creationDate The date of commenting.
     * @param wasUpdated If the comment was updated.
     * @param wasDeleted If the comment was deleted.
     * @param children The children (nested) comments.
     * @param upvotes The number of upvotes it has.
     * @param downvotes The number of downvotes it has.
     * @param vote The current user's vote on it.
     */
    constructor(
        public id: number,
        public user: User,
        public body: string,
        public creationDate: Date,
        public wasUpdated: boolean,
        public wasDeleted: boolean,
        public upvotes: number,
        public downvotes: number,
        public vote: Vote | null = null
    ) {
        super();
        this.children = [];
    }

    /**
     * Recursive helper to find a child however nested it may be.
     * @param id The ID of the child comment to look for.
     */
    public findChild(id: number): Comment | null {
        const found = this.children.find(c => c.id === id);

        if (found != null) {
            return found;
        }

        for (const c of this.children) {
            const deeperFind = c.findChild(id);

            if (deeperFind != null) {
                return deeperFind;
            }
        }

        return null;
    }
}