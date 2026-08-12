import { Modal } from '../Modal';

const inputClass = (hasError) =>
    `rounded-md bg-[#021526] border px-3 py-2 text-[#E2E2B6] focus:outline-none ${
        hasError ? 'border-red-500 focus:border-red-500' : 'border-[#6EACDA]/40 focus:border-[#6EACDA]'
    }`;

const ErrorText = ({ message }) =>
    message ? <span className="text-red-500 text-sm">{message}</span> : null;

export const EditEquipment = ({
    equipment,
    isOpen,
    onClose,
    onChange,
    isSubmitting,
    onSubmit,
    errors
}) => {
    return (
        <Modal isOpen={isOpen} onClose={onClose} title="Edit equipment">
            <div className="flex flex-col gap-3">
                <label htmlFor="edit-id" className="text-sm font-medium">ID</label>
                <input
                    id="edit-id"
                    type="text"
                    readOnly
                    value={equipment ? equipment.equipmentId : ''}
                    className="rounded-md bg-[#021526]/60 border border-[#6EACDA]/20 px-3 py-2 text-[#E2E2B6]/70 cursor-not-allowed"
                />

                <label htmlFor="edit-name" className="text-sm font-medium">Name:</label>
                <input
                    id="edit-name"
                    type="text"
                    name="name"
                    value={equipment ? equipment.name : ''}
                    onChange={onChange}
                    className={inputClass(errors.name)}
                />
                <ErrorText message={errors.name} />

                <label htmlFor="edit-brand" className="text-sm font-medium">Brand:</label>
                <input
                    id="edit-brand"
                    type="text"
                    name="brand"
                    value={equipment ? equipment.brand : ''}
                    onChange={onChange}
                    className={inputClass(errors.brand)}
                />
                <ErrorText message={errors.brand} />

                <label htmlFor="edit-model" className="text-sm font-medium">Model:</label>
                <input
                    id="edit-model"
                    type="text"
                    name="model"
                    value={equipment ? equipment.model : ''}
                    onChange={onChange}
                    className={inputClass(errors.model)}
                />
                <ErrorText message={errors.model} />

                <label htmlFor="edit-serialNumber" className="text-sm font-medium">Serial Number:</label>
                <input
                    id="edit-serialNumber"
                    type="text"
                    name="serialNumber"
                    value={equipment ? equipment.serialNumber : ''}
                    onChange={onChange}
                    className={inputClass(errors.serialNumber)}
                />
                <ErrorText message={errors.serialNumber} />
            </div>

            <div className="flex justify-end gap-2 mt-4">
                <button
                    onClick={onSubmit}
                    disabled={isSubmitting}
                    className="px-4 py-2 rounded-md bg-[#03346E] text-[#E2E2B6] hover:bg-[#6EACDA] hover:text-[#021526] transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
                >
                    {isSubmitting ? 'Editing...' : 'Edit'}
                </button>
                <button
                    onClick={onClose}
                    className="px-4 py-2 rounded-md bg-red-600 text-white hover:bg-red-700 transition-colors"
                >
                    Cancel
                </button>
            </div>
        </Modal>
    );
}