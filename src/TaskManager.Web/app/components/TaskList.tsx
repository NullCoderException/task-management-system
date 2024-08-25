import { Link } from "@remix-run/react";
import { Task } from "~/types/task";

interface TaskListProps {
  tasks: Task[];
}

export default function TaskList({ tasks }: TaskListProps) {
  return (
    <div className="space-y-4">
      {tasks.map((task) => (
        <div key={task.id} className="border p-4 rounded shadow">
          <h3 className="text-lg font-semibold">{task.title}</h3>
          <p className="text-gray-600">{task.description}</p>
          <div className="mt-2 flex justify-between items-center">
            <span
              className={`px-2 py-1 rounded text-sm ${
                task.status === "todo"
                  ? "bg-red-200 text-red-800"
                  : task.status === "in-progress"
                  ? "bg-yellow-200 text-yellow-800"
                  : "bg-green-200 text-green-800"
              }`}
            >
              {task.status}
            </span>
            {task.dueDate && (
              <span className="text-sm text-gray-500">
                Due: {new Date(task.dueDate).toLocaleDateString()}
              </span>
            )}
          </div>
          <Link
            to={`/tasks/${task.id}`}
            className="mt-2 inline-block text-blue-500 hover:underline"
          >
            View Details
          </Link>
        </div>
      ))}
    </div>
  );
}
