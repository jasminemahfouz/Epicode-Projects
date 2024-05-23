import { Component } from '@angular/core';
import { PostInterface } from 'src/interfaces/post.interface';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss'],
})
export class HomepageComponent {
  post!: PostInterface;
  related!: PostInterface[];
  constructor() {
    this.getTopPost().then((res) => (this.post = res));
    this.getRandomPosts(4).then((res) => (this.related = res));
  }
  async getTopPost(): Promise<PostInterface> {
    const res = await fetch('../../assets/db.json');
    const posts = (await res.json()) as Array<PostInterface>;

    return posts.sort((a, b) => b.reactions - a.reactions)[0];
  }
  async getRandomPosts(num: number): Promise<PostInterface[]> {
    const res = await fetch('../../assets/db.json');
    const posts = (await res.json()) as Array<PostInterface>;
    const randomPosts: PostInterface[] = [];
    const numHistory: number[] = [];
    for (let i = 0; i < num; i++) {
      const random = Math.floor(Math.random() * posts.length);
      if (numHistory.includes(random)) continue;
      numHistory.push(random);
      randomPosts.push(posts[random]);
    }
    return randomPosts;
  }

}
