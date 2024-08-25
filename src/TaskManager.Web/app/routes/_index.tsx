import { Link } from "@remix-run/react";

export default function Index() {
  return (
    <div className="text-center">
      <h1 className="text-4xl font-bold mb-4">Welcome to Task Manager</h1>
      <p className="mb-4">
        Manage your tasks efficiently with our easy-to-use application.
      </p>
      <Link
        to="/tasks"
        className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
      >
        View Tasks
      </Link>
    </div>
  );
}
