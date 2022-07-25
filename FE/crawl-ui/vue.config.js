module.exports = {
    publicPath: process.env.NODE_ENV === 'production'
      ? '/crawl.github.io/' // Thay tên repository của các bạn vào đây nhé
      : '/'
  }