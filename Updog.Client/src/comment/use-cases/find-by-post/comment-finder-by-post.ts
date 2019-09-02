import { ApiInteractor } from '@/core/api-interactor';
import { Comment } from '@/comment/common/comment';
import { CommentApiInteractor } from '@/comment/common/comment-api-interactor';
import { CommentFinderByPostParams } from './comment-finder-by-post-params';
import { PagedResultSet } from '@/core/pagination/paged-result-set';

/**
 * Interactor to find a post by it's ID.
 */
export class CommentFinderByPost extends CommentApiInteractor<CommentFinderByPostParams, PagedResultSet<Comment>> {
    public async handle(input: CommentFinderByPostParams): Promise<PagedResultSet<Comment>> {
        const response = await this.http.get<Comment[]>(`/comment/`, {
            params: {
                postId: input.postId,
                pageNumber: input.paginationInfo.pageNumber,
                pageSize: input.paginationInfo.pageSize
            }
        });

        const pagination = this.getPaginationInfo(response);
        const items = response.data.map(ci => this.commentMapper.map(ci));

        return new PagedResultSet(items, pagination);
    }
}
