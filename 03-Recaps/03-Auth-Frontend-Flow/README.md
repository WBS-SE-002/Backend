# Deployment Guide for React SPA and Express Servers

### Deployed Links

- [Travel Journal SPA](https://travel-journal-spa-se02.onrender.com/)
- [Travel Journal Auth Server](https://travel-journal-auth-server-se02.onrender.com/)
- [Travel Journal API](https://backend-jfhu.onrender.com/)

So far, you’ve deployed simple projects via GitHub Pages, and got an introduction to deploying a Parcel application on Render. Luckily, for deploying a React app, the basics are the same! There are a couple more considerations to account for, so we’ll highlight those, and then introduce how to deploy a web service (backend) as well.

### Repository Architecture

When creating a full-stack application using the tech stack we’ve been working with in the bootcamp, you have two major options for setting up your GitHub repositories

1. Have 2 repos - one for the frontend, a second for the backend
2. Have 1 repo (a monorepo) - then create nested `backend` and `frontend` directories

Both have their pros and cons, but in terms of deployment the only significant difference comes to the Root Directory that you will be deploying from. If you have separate repos for frontend and backend, then you don’t need to specify a root when deploying, but if using a monorepo then you have to specify that for the frontend the root is now the `frontend` folder, and the backend root is the `backend` folder. You could even create one giant monorepo that includes several projects, and then deploy based on the path to that single project (some companies organize their code in this way). When relevant, the following guide will note how to specify the root directory.

### Deploying a Static Site (React Frontend)

- Sign into your Render account (revisit [this tutorial](https://learn.wbscodingschool.com/courses/software-engineering/lessons/javascript-modules/topic/%f0%9f%a7%a9-deployment/) for signing up for an account) and
- You’ll land here:

  ![Screenshot 2025-04-15 121905.png](https://learn.wbscodingschool.com/wp-content/uploads/2025/04/image-42-1024x253.webp)

- Click `Add new` and select `Static Site`
- Use the `Git Provider` search bar to find your project repository
  ![Screenshot 2025-04-15 123006.png](https://learn.wbscodingschool.com/wp-content/uploads/2025/04/image-44.webp)
- If the repo contains only the frontend, you can leave the `Root Directory` blank and use the following configuration
  - **Branch:** the branch Render will pull code from and assume as production
  - **Publish directory:** for us, `dist`

![](https://learn.wbscodingschool.com/wp-content/uploads/2025/04/image-46-1024x412.webp)

- If you’re using a monorepo, you will need to specify the root

![Screenshot 2025-04-15 123533.png](https://learn.wbscodingschool.com/wp-content/uploads/2025/04/image-48-1024x293.webp)

### Add your environmental variables (if they exist)

- If you have a `.env.local` file, you will also need to add your environmental variables to the deployed site. You can add them individually, but it’s easiest to choose `Add from .env` and copy/paste the contents of your `.env.local` file
  ![Screenshot 2025-04-15 124835.png](https://learn.wbscodingschool.com/wp-content/uploads/2025/04/image-50.webp)
- Note that if you are using a backend of your own creation, that you will need to update the URL to the deployed backend

![Screenshot 2025-04-15 124848.png](https://learn.wbscodingschool.com/wp-content/uploads/2025/04/image-51-1024x248.webp)

- Scroll down and click `Deploy Static Site`
- By default, whenever you integrate new changes into `main` render will trigger a build process to keep your site up to date. Learn more about this and more in [their official documentation](https://docs.render.com/deploys)

### Add routing (if necessary)

If you are using React Router to handle routing, you’ll need one extra step for the client side routing to work properly

- Select `Redirects/Rewrites` from the sidenav, the click `Add rule`
- Use the following config

  ![Screenshot 2025-04-15 125825.png](https://learn.wbscodingschool.com/wp-content/uploads/2025/04/image-53-1024x218.webp)

- Click `Save Changes`

Another option for deploying static sites is [Netlify](https://www.netlify.com/). We won’t go into the details of deployment, since they are largely similar to Render, but they handle routing in a slightly different way

- Create a file called `_redirects` inside of the `public` folder with the following content `/* /index.html 200`

![Screenshot 2025-04-15 130047.png](https://learn.wbscodingschool.com/wp-content/uploads/2025/04/image-55.webp)

### Deploying a Web Service (Express.js Backend)

The process for deploying your backend will look largely the same, but with some minor differences.

- Click `New` and this time select `Web Service`
- Choose a Git Provider (just as with the static site)
- We’ll use the following configuration
  - **Language**: Node
  - **Region:** Frankfurt (to reduce latency)
  - **Root Directory:** blank, or the path to your backend root directory
  - **Build Command:** `npm i ; npm run build`
  - **Start Command:** `npm run start`
  - Select the free tier (though note that this means your service will go to sleep when inactive. If presenting, always make sure to ping your backend right before to wake it up. It can take up to a minute for your free tier backend to wake up.)
- **_Environmental Variables_** you will want to click `Advanced` then `Add Secrets File` and name it `.env.production.local` and add the contents of your `.env.local` file there (remember to update client URLs to the deployed URL)
- Click `Deploy Web Service`
- Just as with Static Sites, this will auto-redeploy when a new commit is added to `main`

### Troubleshooting Common Issues

First things first, always check the logs! You can find them if you click on `Logs` from the sidenav, and they’ll look something like this

![Screenshot 2025-04-16 104950.png](https://learn.wbscodingschool.com/wp-content/uploads/2025/04/image-57-1024x361.webp)

You can also check the logs for specific deployments by clicking on `Events` from the sidenav

### On my static site, I get a 404 if I refresh the page anywhere except the homepage

Double check you’ve added Redirects/Rewrites

### I get an error indicating a missing variable (i.e. a Mongoose error that you’re missing the connection string)

Make sure you’ve added you’re environmental variables!

### I get a `MongooseServerSelectionError`

![Screenshot 2025-04-16 105052.png](https://learn.wbscodingschool.com/wp-content/uploads/2025/04/image-61-1024x88.webp)

- This means that the IP Address of the deployed server isn’t allowed Network Access to your database. To fix this, log into your MongoDB Atlas account. From the sidenav under `Security` select `Network Access`
  ![Screenshot 2025-04-16 105404.png](https://learn.wbscodingschool.com/wp-content/uploads/2025/04/image-63-1024x278.webp)
- Then click `Add IP Address` and in the modal choose `Allow Access From Anywhere` which will be `0.0.0.0/0`
- Trigger a manual redeploy

### I get a `Module Not Found` error, but everything runs fine locally

- The examples use JS files, but the same applies with TS

![Screenshot 2025-04-16 114916.png](https://learn.wbscodingschool.com/wp-content/uploads/2025/04/image-65-1024x126.webp)

The OS on your computer may not be case sensitive when it comes to file names, but Render IS case sensitive. So if I have a file named `WildDuckRouter.js` , I can import it like this just fine while running the server locally, but it will throw an error when trying to deploy

```jsx
import wildDuckRouter from './routes/wildDuckRouter.js';
```

Simply rename the file, or fix the import (depending on which casing you want to the file to have). In our case, renaming the file to `wildDuckRouter.js` will solve the issue.

### I have a different error that you didn’t mention

Check the logs for the error message, and try to understand it. Some error messages are clearer than others, but it can generally give you a clue on where to look to fix the bug. If you need some extra guidance, copy the text of the error into search to see if someone else had the same issue and resolved it. Next step is to throw the error message into ChatGPT (or your AI chatbot of choice). If you’re still stuck, reach out to your instructor and make sure to include

- A description of your problem, and the steps you’ve already tried to resolve it
- A screenshot of your logs that includes the full error message
