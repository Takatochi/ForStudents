CREATE TABLE users ( 
    id SERIAL PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    password_hash TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


CREATE TABLE posts (
    id SERIAL PRIMARY KEY,
    user_id INT NOT NULL,
    content TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);


CREATE TABLE comments (
    id SERIAL PRIMARY KEY,
    post_id INT NOT NULL,
    user_id INT NOT NULL,
    content TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (post_id) REFERENCES posts(id) ON DELETE CASCADE,
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);


CREATE TABLE likes (
    id SERIAL PRIMARY KEY,
    post_id INT,
    comment_id INT,
    user_id INT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT unique_like UNIQUE (user_id, post_id, comment_id),
    FOREIGN KEY (post_id) REFERENCES posts(id) ON DELETE CASCADE,
    FOREIGN KEY (comment_id) REFERENCES comments(id) ON DELETE CASCADE,
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);


CREATE TABLE follows (
    id SERIAL PRIMARY KEY,
    follower_id INT NOT NULL,
    following_id INT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (follower_id, following_id),
    FOREIGN KEY (follower_id) REFERENCES users(id) ON DELETE CASCADE,
    FOREIGN KEY (following_id) REFERENCES users(id) ON DELETE CASCADE
);


INSERT INTO users (username, email, password_hash) VALUES
('ivan_petrenko', 'ivan.petrenko@email.com', 'hash1'),
 ('maria_ivanenko', 'maria.ivanenko@email.com', 'hash2'),
 ('oleg_shevchenko', 'oleg.shevchenko@email.com', 'hash3');

-- Додавання постів
INSERT INTO posts (user_id, content) VALUES
 (1, 'Мій перший пост у цій соцмережі!'),
 (2, 'Привіт усім! Як справи?'),
 (3, 'Щойно прочитав цікаву книгу, рекомендую.');

-- Додавання коментарів
INSERT INTO comments (post_id, user_id, content) VALUES
  (1, 2, 'Вітаю! Раді бачити тебе тут.'),
  (1, 3, 'Гарний пост, продовжуй писати!'),
  (2, 1, 'Все чудово, дякую! А у тебе як?');

-- Додавання лайків
INSERT INTO likes (user_id, post_id, comment_id) VALUES
  (2, 1, NULL), -- Марія лайкнула пост Івана
  (3, 1, NULL), -- Олег лайкнув пост Івана
  (1, NULL, 1), -- Іван лайкнув коментар Марії
  (3, NULL, 2); -- Олег лайкнув коментар свій

-- Додавання підписок
INSERT INTO follows (follower_id, following_id) VALUES
   (1, 2), -- Іван підписався на Марію
   (1, 3), -- Іван підписався на Олега
   (2, 1), -- Марія підписалася на Івана
   (3, 2); -- Олег підписався на Марію