import { Link } from "@remix-run/react";

export default function Layout({ children }: { children: React.ReactNode }) {
  return (
    <div className="flex flex-col min-h-screen">
      <header className="bg-blue-600 text-white p-4">
        <nav className="container mx-auto flex justify-between items-center">
          <Link to="/" className="text-2xl font-bold">
            Task Manager
          </Link>
          <ul className="flex space-x-4">
            <li>
              <Link to="/tasks" className="hover:underline">
                Tasks
              </Link>
            </li>
            <li>
              <Link to="/tasks/new" className="hover:underline">
                New Task
              </Link>
            </li>
          </ul>
        </nav>
      </header>
      <main className="flex-grow container mx-auto mt-4 p-4">{children}</main>
      <footer className="bg-gray-200 p-4 text-center">
        <p>&copy; 2024 Task Manager</p>
      </footer>
    </div>
  );
}
